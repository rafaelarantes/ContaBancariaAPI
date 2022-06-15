using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Data.Dtos.Conta;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IBancoMapper
    {
        NovaContaDto Map(NovaContaViewModel novaContaViewModel);
    }
}
