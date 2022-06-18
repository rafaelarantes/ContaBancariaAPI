using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using System;
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
            return await Obter(guid);
        }

        public async Task<RetornoDto> Atualizar(Conta conta)
        {
            return await Atualizar(conta);
        }
    }
}
