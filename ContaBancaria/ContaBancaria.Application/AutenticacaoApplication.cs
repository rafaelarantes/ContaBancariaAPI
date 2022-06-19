using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Autenticacao;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Helper;
using ContaBancaria.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class AutenticacaoApplication : IAutenticacaoApplication
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AutenticacaoApplication(IUsuarioRepository usuarioRepository,
                                       IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }


        public async Task<string> Autenticar(AutenticacaoLoginViewModel autenticacaoLoginViewModel)
        {
            var senhaHash = HashHelper.GerarSha1(autenticacaoLoginViewModel.Senha);

            var usuario = await _usuarioRepository.Obter(autenticacaoLoginViewModel.Login, senhaHash);
            if (usuario == null) return default;

            return GerarToken(usuario);
        }

        private string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration.GetSection("Secret").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Autorizacao.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
