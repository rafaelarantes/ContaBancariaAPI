using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System;

namespace ContaBancaria.Application
{
    public class BancoApplication : IBancoApplication
    {
        private readonly IBancoMapper _bancoMapper;

        public BancoApplication(IBancoMapper bancoMapper)
        {
            _bancoMapper = bancoMapper;
        }

        public void CadastrarBanco()
        {

        }
        public void ListarBancos()
        {

        }

        public RetornoViewModel Depositar(Conta conta, decimal valor)
        {
            throw new NotImplementedException();
        }

        public RetornoViewModel Transferir(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            throw new NotImplementedException();
        }

        public RetornoViewModel Sacar(Conta conta, decimal valor)
        {
            throw new NotImplementedException();
        }

        public ExtratoViewModel VisualizarExtrato(Conta conta)
        {
            throw new NotImplementedException();
        }
    }
}
