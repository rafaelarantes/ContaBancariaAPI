using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public class BancoRepository : Repository<Banco>, IBancoRepository
    {
    }
}
