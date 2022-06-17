using ContaBancaria.Data.Contexts;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public abstract class Repository<T> where T: Entity
    {
        protected BancoContext _bancoContext;

        public Repository()
        {
            _bancoContext = new BancoContext();
        }

        public async Task<RetornoDto> Incluir(T entity)
        {
            await _bancoContext.AddAsync(entity);

            return new RetornoDto
            {
                Resultado = true
            };
        }

        public async Task<RetornoDto> Atualizar(T entity)
        {
            await Task.Run(() => _bancoContext.Set<T>().Update(entity));
            
            return new RetornoDto
            {
                Resultado = true
            };
        }

        public async Task<IEnumerable<T>> Listar()
        {
            return await _bancoContext.Query<T>().AsNoTracking().ToListAsync();
        }

        public async Task<RetornoDto> Excluir(Guid guid)
        {
            var item = await _bancoContext.Query<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Guid == guid);
            _bancoContext.Set<T>().Remove(item);

            return new RetornoDto
            {
                Resultado = true
            };
        }

        public async Task<T> Obter(Guid guid)
        {
            return await _bancoContext.Query<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Guid == guid);
        }
    }
}
