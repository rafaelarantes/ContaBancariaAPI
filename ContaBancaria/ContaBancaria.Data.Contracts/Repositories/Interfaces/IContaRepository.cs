using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IContaRepository
    {
        Task<Conta> ObterInclude(Guid guidContaDestino);

        Task<RetornoDto> Gravar();

        Task<RetornoDto> Incluir(Conta conta);
        
        Task<RetornoDto> Excluir(Guid guid);

        Task<IEnumerable<Conta>> ListarInclude();
    }
}
