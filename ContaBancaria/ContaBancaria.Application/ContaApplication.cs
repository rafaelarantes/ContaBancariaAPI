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

        public ContaApplication(IBancoApplication bancoApplication,
                                IContaMapper contaMapper,
                                IContaRepository contaRepository)
        {
            _bancoApplication = bancoApplication;
            _contaMapper = contaMapper;
            _contaRepository = contaRepository;
        }

        public async Task<RetornoViewModel> Depositar(DepositoViewModel depositoViewModel)
        {
            var conta = await _contaRepository.Obter(depositoViewModel.GuidContaDestino);
            var depositoBancarioViewModel = _contaMapper.Map(depositoViewModel, conta);

            return await _bancoApplication.Depositar(depositoBancarioViewModel);
        }

        public async Task<RetornoViewModel> Sacar(SaqueViewModel saqueViewModel)
        {
            var conta = await _contaRepository.Obter(saqueViewModel.GuidContaDestino);
            var saqueBancarioViewModel = _contaMapper.Map(saqueViewModel, conta);

            return await _bancoApplication.Sacar(saqueBancarioViewModel);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaViewModel transferenciaViewModel)
        {
            var contaOrigem = await _contaRepository.Obter(transferenciaViewModel.GuidContaOrigem);
            var contaDestino = await _contaRepository.Obter(transferenciaViewModel.GuidContaDestino);
            
            var transferenciaBancariaViewModel = _contaMapper.Map(contaOrigem, contaDestino, transferenciaViewModel);

            return await _bancoApplication.Transferir(transferenciaBancariaViewModel); 
        }

        public async Task<ExtratoViewModel> VisualizarExtrato(Guid guidConta)
        {
            var conta = await _contaRepository.Obter(guidConta);
            return _contaMapper.Map(conta.Extrato);
        }
    }
}
