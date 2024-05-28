using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Dtos;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IRetornoMapper
    {
        RetornoViewModel Map(object data, bool resultado = true);

        RetornoViewModel Map(RetornoDto retornoDto);

        RetornoViewModel Map(bool resultado, List<string> mensagem);
    }
}
