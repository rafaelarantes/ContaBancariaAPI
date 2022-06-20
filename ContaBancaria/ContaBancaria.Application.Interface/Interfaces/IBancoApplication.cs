using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IBancoApplication
    {
        Task<RetornoViewModel> Transferir(TransferenciaBancariaViewModel transferenciaBancariaViewModel);
        Task<RetornoViewModel> Depositar(DepositoBancarioViewModel depositoBancarioViewModel);
        Task<RetornoViewModel> Sacar(SaqueBancarioViewModel saqueBancarioViewModel);
        Task<IEnumerable<ContaViewModel>> ListarContas();
        Task<RetornoViewModel> CriarConta(NovaContaViewModel novaContaViewModel);
        Task<RetornoViewModel> ExcluirConta(Guid guid);
        Task<RetornoViewModel> ReceberTransferencia(DepositoBancarioViewModel depositoBancarioViewModel);
    }
}
