using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class ContaApplication : IContaApplication
    {
        private readonly IBancoApplication _bancoApplication;

        public ContaApplication(IBancoApplication bancoApplication)
        {
            _bancoApplication = bancoApplication;
        }

        public async Task<RetornoViewModel> Depositar(DepositoViewModel depositoViewModel)
        {
            var conta = new Conta(0);

            return await _bancoApplication.Depositar(conta, 1000.5M);
        }

        public async Task<RetornoViewModel> Sacar(SaqueViewModel saqueViewModel)
        {
            var conta = new Conta(0);

            return await _bancoApplication.Sacar(conta, 1000.5M);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaViewModel transferenciaViewModel)
        {
            var contaOrigem = new Conta(0);
            var contaDestino = new Conta(0);

            return await _bancoApplication.Transferir(contaOrigem, contaDestino, 1000.5M); 
        }

        public async Task<ExtratoViewModel> VisualizarExtrato()
        {
            var conta = new Conta(0);

            return await _bancoApplication.VisualizarExtrato(conta);
        }
    }
}
