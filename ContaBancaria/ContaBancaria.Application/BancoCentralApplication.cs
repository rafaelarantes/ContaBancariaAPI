using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoCentralApplication : IBancoCentralApplication
    {
        private readonly IBancoMapper _bancoMapper;
        private readonly IBancoRepository _bancoRepository;
        private readonly IRetornoMapper _retornoMapper;

        public BancoCentralApplication(IBancoMapper bancoMapper,
                        IBancoRepository bancoRepository,
                        IRetornoMapper retornoMapper)
        {
            _bancoMapper = bancoMapper;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
        }

        public async Task<IEnumerable<BancosViewModel>> ListarBancos()
        {
            var bancos = await _bancoRepository.Listar();
            return _bancoMapper.Map(bancos);
        }

        public async Task<RetornoViewModel> CriarBanco(NovoBancoViewModel novoBancoViewModel)
        {
            var banco = _bancoMapper.Map(novoBancoViewModel);

            var retornoDto = await _bancoRepository.Incluir(banco);
            return _retornoMapper.Map(retornoDto);
        }

        public async Task<RetornoViewModel> ExcluirBanco(Guid guid)
        {
            var retornoDto = await _bancoRepository.Excluir(guid);
            return _retornoMapper.Map(retornoDto);
        }

        public async Task<RetornoViewModel> Transferir(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            var creditado = await contaDestino.Creditar(valor);
            if (!creditado) return _retornoMapper.Map(creditado);

            var atualizou = await _bancoRepository.AtualizarConta(contaDestino);
            return _retornoMapper.Map(atualizou);
        }
    }
}
