using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IFilaProcessamentoDbRepository
    {
        Task<RetornoDto> Incluir(FilaProcessamento serviceBusDto);

        Task<RetornoDto> Gravar(FilaProcessamento processamento);

        void FinalizarTransacao();

        IEnumerable<FilaProcessamento> ListarPendenteTracking();

        void Rollback();
    }
}
