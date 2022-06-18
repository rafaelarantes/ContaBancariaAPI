using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IBancoRepository
    {
        Task<RetornoDto> Excluir(Guid guid);
        Task<RetornoDto> Incluir(Banco banco);
        Task<IEnumerable<Banco>> Listar();
    }
}
