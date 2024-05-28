using ContaBancaria.Application.Contracts.ViewModels.Conta;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IContaApplication
    {
        Task<RetornoViewModel> VisualizarExtrato(Guid guidConta);

        Task<RetornoViewModel> Depositar(DepositoViewModel depositoViewModel);
        
        Task<RetornoViewModel> Sacar(SaqueViewModel saqueViewModel);

        Task<RetornoViewModel> Transferir(TransferenciaViewModel transferenciaViewModel);
    }
}
