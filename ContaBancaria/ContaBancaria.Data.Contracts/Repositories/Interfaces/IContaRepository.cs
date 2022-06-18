using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta> Obter(Guid guidContaDestino);

        Task<RetornoDto> Atualizar(Conta conta);

        Task<RetornoDto> Incluir(Conta conta);
        
        Task<RetornoDto> Excluir(Guid guid);

        Task<IEnumerable<Conta>> Listar();
    }
}
