using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;

namespace ContaBancaria.Application
{
    public class ContaApplication : IContaApplication
    {
        private readonly IBancoApplication _bancoApplication;

        public ContaApplication(IBancoApplication bancoApplication)
        {
            _bancoApplication = bancoApplication;
        }

        public RetornoViewModel Depositar(DepositoViewModel depositoViewModel)
        {
            var conta = new Conta(0);

            return _bancoApplication.Depositar(conta, 1000.5M);
        }

        public RetornoViewModel Sacar(SaqueViewModel saqueViewModel)
        {
            var conta = new Conta(0);

            return _bancoApplication.Sacar(conta, 1000.5M);
        }

        public RetornoViewModel Transferir(TransferenciaViewModel transferenciaViewModel)
        {
            var contaOrigem = new Conta(0);
            var contaDestino = new Conta(0);

            return _bancoApplication.Transferir(contaOrigem, contaDestino, 1000.5M); 
        }

        public ExtratoViewModel VisualizarExtrato()
        {
            var conta = new Conta(0);

            return _bancoApplication.VisualizarExtrato(conta);
        }
    }
}
