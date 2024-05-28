using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class ContaApplication : IContaApplication
    {
        private readonly IBancoApplication _bancoApplication;
        private readonly IContaMapper _contaMapper;
        private readonly IContaRepository _contaRepository;
        private readonly IRetornoMapper _retornoMapper;

        public ContaApplication(IBancoApplication bancoApplication,
                                IContaMapper contaMapper,
                                IContaRepository contaRepository,
                                IRetornoMapper retornoMapper)
        {
            _bancoApplication = bancoApplication;
            _contaMapper = contaMapper;
            _contaRepository = contaRepository;
            _retornoMapper = retornoMapper;
        }

        public async Task<RetornoViewModel> Depositar(DepositoViewModel depositoViewModel)
        {
            var depositoBancarioViewModel = _contaMapper.Map(depositoViewModel);
            return await _bancoApplication.Depositar(depositoBancarioViewModel);
        }

        public async Task<RetornoViewModel> Sacar(SaqueViewModel saqueViewModel)
        {
            var saqueBancarioViewModel = _contaMapper.Map(saqueViewModel);
            return await _bancoApplication.Sacar(saqueBancarioViewModel);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaViewModel transferenciaViewModel)
        {
            var transferenciaBancariaViewModel = _contaMapper.Map(transferenciaViewModel);
            return await _bancoApplication.Transferir(transferenciaBancariaViewModel); 
        }

        public async Task<RetornoViewModel> VisualizarExtrato(Guid guidConta)
        {
            var conta = await _contaRepository.ObterInclude(guidConta);
            var extratoViewModel = _contaMapper.Map(conta);

            return _retornoMapper.Map(extratoViewModel);
        }
    }
}
