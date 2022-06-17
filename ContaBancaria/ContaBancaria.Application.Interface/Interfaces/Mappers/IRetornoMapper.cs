using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Dtos;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IRetornoMapper
    {
        RetornoViewModel Map(RetornoDto retornoDto);

        RetornoViewModel Map(bool resultado);
    }
}
