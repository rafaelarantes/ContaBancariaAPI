using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public class ContaRepository : Repository, IContaRepository
    {
        public ContaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Conta> ObterInclude(Guid guid)
        {
            return await _bancoContext.Set<Conta>()
                                      .Include(i => i.Extrato)
                                      .Include(i => i.Banco)
                                      .Include(i => i.Banco.TaxasBancarias)
                                      .SingleOrDefaultAsync(x => x.Guid == guid);
        }

        public async Task<RetornoDto> Atualizar(Conta conta)
        {
            return await Atualizar<Conta>(conta);
        }

        public async Task<RetornoDto> Incluir(Conta conta)
        {
            return await Incluir<Conta>(conta);
        }

        public async Task<RetornoDto> Excluir(Guid guid)
        {
            return await Excluir<Conta>(guid);
        }

        public async Task<IEnumerable<Conta>> ListarInclude()
        {
            return await _bancoContext.Set<Conta>()
                                      .Include(x => x.Extrato)
                                      .AsNoTracking()
                                      .ToListAsync();
        }
    }
}
