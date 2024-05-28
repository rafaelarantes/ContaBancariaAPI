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
            return await Excluir<Banco>(guid);
        }

        public async Task<RetornoDto> Incluir(Banco banco)
        {
            return await Incluir<Banco>(banco);
        }

        public async Task<IEnumerable<Banco>> Listar()
        {
            return await Listar<Banco>();
        }

        public async Task<Banco> Obter(Guid guid)
        {
            return await Obter<Banco>(guid);
        }
    }
}
