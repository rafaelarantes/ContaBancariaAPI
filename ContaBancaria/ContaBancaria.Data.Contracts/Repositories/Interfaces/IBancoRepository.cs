using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IBancoRepository
    {
        Task<IEnumerable<Banco>> Listar();

        Task<RetornoDto> Incluir(Banco banco);
        Task<RetornoDto> Excluir(Guid guid);
    }
}
