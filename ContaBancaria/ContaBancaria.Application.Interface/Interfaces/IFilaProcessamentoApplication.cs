using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Enums;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IFilaProcessamentoApplication
    {
        Task<RetornoDto> Enfileirar(TipoComandoFila tipoComandoFila, string dados);
    }
}
