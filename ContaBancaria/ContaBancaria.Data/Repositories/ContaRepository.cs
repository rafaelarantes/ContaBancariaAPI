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

        public async Task<Conta> Obter(Guid guid)
        {
            return await Obter<Conta>(guid);
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

        public async Task<IEnumerable<Conta>> Listar()
        {
            return await _bancoContext.Set<Conta>()
                                      .Include(x => x.Extrato)
                                      .AsNoTracking()
                                      .ToListAsync();
        }
    }
}
