using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public class BancoRepository : Repository, IBancoRepository
    {
        public BancoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<RetornoDto> Excluir(Guid guid)
        {
            return await Excluir(guid);
        }

        public async Task<RetornoDto> Incluir(Banco banco)
        {
            return await Incluir(banco);
        }

        public async Task<IEnumerable<Banco>> Listar()
        {
            return await Listar();
        }
    }
}
