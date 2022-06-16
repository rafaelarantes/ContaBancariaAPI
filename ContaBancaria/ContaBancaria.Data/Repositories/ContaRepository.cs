using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;

namespace ContaBancaria.Data.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
    }
}
