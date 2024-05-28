using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Autenticacao;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Data.Helper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class AutenticacaoApplication : IAutenticacaoApplication
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IRetornoMapper _retornoMapper;

        public AutenticacaoApplication(IUsuarioRepository usuarioRepository,
                                       IConfiguration configuration,
                                       IRetornoMapper retornoMapper)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _retornoMapper = retornoMapper;
        }


        public async Task<RetornoViewModel> Autenticar(AutenticacaoLoginViewModel autenticacaoLoginViewModel)
        {
            var senhaHash = HashHelper.GerarSha1(autenticacaoLoginViewModel.Senha);

            var usuario = await _usuarioRepository.Obter(autenticacaoLoginViewModel.Login, senhaHash);
            if (usuario == null) 
                return _retornoMapper.Map(false, new List<string>
                                                 {
                                                     "Usuário ou senha incorretos."
                                                 });

            var secret = _configuration.GetSection("Secret").Value;

            var token = TokenHelper.GerarToken(usuario.Login, usuario.Autorizacao, secret);

            return _retornoMapper.Map(new AutenticacaoViewModel
            {
                Token = token,
                Autorizacao = usuario.Autorizacao
            }, !string.IsNullOrWhiteSpace(token));
        }
    }
}
