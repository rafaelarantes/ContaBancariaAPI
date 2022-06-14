using ContaBancaria.Application.Contratos;
using ContaBancaria.Application.Contratos.ViewModels;

namespace ContaBancaria.Application
{
    public class ContaApplication : IContaApplication
    {
        public RetornoViewModel Depositar(DepositoViewModel depositoViewModel)
        {
            throw new System.NotImplementedException();
        }

        public RetornoViewModel Sacar(SaqueViewModel saqueViewModel)
        {
            throw new System.NotImplementedException();
        }

        public RetornoViewModel Transferir(TransferenciaViewModel transferenciaViewModel)
        {
            throw new System.NotImplementedException();
        }

        public ExtratoViewModel VisualizarExtrato()
        {
            return new ExtratoViewModel();
        }
    }
}
