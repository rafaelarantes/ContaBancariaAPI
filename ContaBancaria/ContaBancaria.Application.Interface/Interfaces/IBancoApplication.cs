using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IBancoApplication
    {
        RetornoViewModel Transferir(Conta contaOrigem, Conta contaDestino, decimal valor);
        RetornoViewModel Depositar(Conta conta, decimal v);
        RetornoViewModel Sacar(Conta conta, decimal v);
        ExtratoViewModel VisualizarExtrato(Conta conta);
    }
}
