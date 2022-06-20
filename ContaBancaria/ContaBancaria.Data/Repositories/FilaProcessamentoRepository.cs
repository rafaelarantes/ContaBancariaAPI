using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public class FilaProcessamentoRepository : Repository, IFilaProcessamentoRepository
    {
        public FilaProcessamentoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<RetornoDto> Incluir(FilaProcessamento filaProcessamento)
        {
            return await Incluir<FilaProcessamento>(filaProcessamento);
        }

        public IEnumerable<FilaProcessamento> ListarPendenteTracking()
        {
            var filas = ListarTracking<FilaProcessamento>(
                t => t.Situacao == SituacaoFilaProcessamento.Enfileirado || 
                     t.Situacao == SituacaoFilaProcessamento.Erro);
            return filas.OrderByDescending(f => f.DataGeracao);
        }

        public override async Task<RetornoDto> Gravar()
        {
            var gravou = await _bancoContext.SaveChangesAsync();

            return new RetornoDto
            {
                Resultado = gravou > 0
            };
        }

        public void FinalizarTransacao()
        {
            _dbContextTransaction.Commit();
            _dbContextTransaction = _bancoContext.Database.BeginTransaction();
        }
    }
}
