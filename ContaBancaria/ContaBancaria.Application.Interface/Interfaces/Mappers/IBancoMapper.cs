using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IBancoMapper
    {
        ListarBancosViewModel Map(IEnumerable<Banco> bancos);
        Banco Map(NovoBancoViewModel novoBancoViewModel);
        RetornoViewModel Map(RetornoDto retornoDto);
    }
}
