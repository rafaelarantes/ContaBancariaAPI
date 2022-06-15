using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;

namespace ContaBancaria.Application.Mappers
{
    public class BancoMapper : IBancoMapper
    {
        public ListarBancosViewModel Map(IEnumerable<Banco> bancos)
        {
            return new ListarBancosViewModel();
        }

        public Banco Map(NovoBancoViewModel novoBancoViewModel)
        {
            return new Banco(novoBancoViewModel.Nome, novoBancoViewModel.numero, novoBancoViewModel.agencia);
        }

        public RetornoViewModel Map(RetornoDto retornoDto)
        {
            return new RetornoViewModel();
        }

    }
}
