using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public abstract class Repository<T> where T: Entity
    {
        public async Task<RetornoDto> Incluir(T entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RetornoDto> Atualizar(T entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> Listar()
        {
            throw new System.NotImplementedException();
        }

        public async Task<RetornoDto> Excluir(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Obter(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
