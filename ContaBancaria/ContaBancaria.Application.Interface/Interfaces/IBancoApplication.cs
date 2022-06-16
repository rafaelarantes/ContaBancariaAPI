using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IBancoApplication
    {
        Task<RetornoViewModel> Transferir(Conta contaOrigem, Conta contaDestino, decimal valor);
        Task<RetornoViewModel> Depositar(Conta conta, decimal v);
        Task<RetornoViewModel> Sacar(Conta conta, decimal v);
        Task<IEnumerable<BancosViewModel>> ListarBancos();
        Task<RetornoViewModel> CriarBanco(NovoBancoViewModel novoBancoViewModel);
        Task<RetornoViewModel> ExcluirBanco(Guid guid);
    }
}
