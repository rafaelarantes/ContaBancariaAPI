using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IFilaProcessamentoRepository
    {
        Task<RetornoDto> Incluir(FilaProcessamento serviceBusDto);
        
        Task<RetornoDto> Gravar();

        void FinalizarTransacao();

        IEnumerable<FilaProcessamento> ListarPendenteTracking();
    }
}
