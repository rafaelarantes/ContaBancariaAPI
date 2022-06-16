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
        public IEnumerable<BancosViewModel> Map(IEnumerable<Banco> bancos)
        {
            return new List<BancosViewModel>();
        }

        public Banco Map(NovoBancoViewModel novoBancoViewModel)
        {
            return new Banco(novoBancoViewModel.Nome, novoBancoViewModel.NumeroBanco, novoBancoViewModel.Agencia);
        }
    }
}
