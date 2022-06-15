using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Data.Dtos.Conta;

namespace ContaBancaria.Application.Mappers
{
    public class BancoMapper : IBancoMapper
    {
        public NovaContaDto Map(NovaContaViewModel novaContaViewModel)
        {
            return new NovaContaDto();
        }
    }
}
