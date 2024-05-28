using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Autenticacao;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Helper;
using Microsoft.Extensions.Configuration;
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

            var secret = _configuration.GetSection("Secret").Value;
            return TokenHelper.GerarToken(usuario.Login, usuario.Autorizacao, secret);
        }

        
    }
}
