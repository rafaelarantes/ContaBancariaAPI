using ContaBancaria.Application.Contratos.ViewModels;

namespace ContaBancaria.Application.Contratos
{
    public interface IContaApplication
    {
        ExtratoViewModel VisualizarExtrato();
        
        RetornoViewModel Depositar(DepositoViewModel depositoViewModel);
        
        RetornoViewModel Sacar(SaqueViewModel saqueViewModel);

        RetornoViewModel Transferir(TransferenciaViewModel transferenciaViewModel);
    }
}
