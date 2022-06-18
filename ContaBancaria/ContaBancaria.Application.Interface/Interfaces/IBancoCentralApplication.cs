using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IBancoCentralApplication
    {
        Task<IEnumerable<BancosViewModel>> ListarBancos();
        Task<RetornoViewModel> CriarBanco(NovoBancoViewModel novoBancoViewModel);
        Task<RetornoViewModel> ExcluirBanco(Guid guid);
        Task<RetornoViewModel> Transferir(Conta contaOrigem, Conta contaDestino, decimal valor);
    }
}
