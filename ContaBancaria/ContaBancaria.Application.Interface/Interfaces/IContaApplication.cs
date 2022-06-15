using ContaBancaria.Application.Contracts.ViewModels.Conta;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IContaApplication
    {
        ExtratoViewModel VisualizarExtrato();
        
        RetornoViewModel Depositar(DepositoViewModel depositoViewModel);
        
        RetornoViewModel Sacar(SaqueViewModel saqueViewModel);

        RetornoViewModel Transferir(TransferenciaViewModel transferenciaViewModel);
    }
}
