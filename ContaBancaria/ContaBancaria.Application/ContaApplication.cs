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
            var conta = await _contaRepository.Obter(depositoViewModel.GuidConta);
            return await _bancoApplication.Depositar(conta, depositoViewModel.Valor);
        }

        public async Task<RetornoViewModel> Sacar(SaqueViewModel saqueViewModel)
        {
            var conta = await _contaRepository.Obter(saqueViewModel.GuidConta);
            return await _bancoApplication.Sacar(conta, saqueViewModel.Valor);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaViewModel transferenciaViewModel)
        {
            var contaOrigem = await _contaRepository.Obter(transferenciaViewModel.GuidContaOrigem);
            var contaDestino = await _contaRepository.Obter(transferenciaViewModel.GuidContaDestino);

            return await _bancoApplication.Transferir(contaOrigem, contaDestino, transferenciaViewModel.Valor); 
        }

        public async Task<ExtratoViewModel> VisualizarExtrato(Guid guidConta)
        {
            var conta = await _contaRepository.Obter(guidConta);

            return _contaMapper.Map(conta.Extrato);
        }
    }
}
