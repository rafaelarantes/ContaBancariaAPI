using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IRetornoMapper
    {
        RetornoViewModel Map(RetornoDto retornoDto);

        RetornoViewModel Map(bool resultado);
    }
}
