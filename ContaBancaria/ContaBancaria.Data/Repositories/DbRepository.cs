using ContaBancaria.Data.Contexts;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public class DbRepository : Repository
    {
        protected BancoContext _bancoContext;
        protected IDbContextTransaction _dbContextTransaction;

        public DbRepository(IConfiguration configuration)
        {
            _bancoContext = new BancoContext(configuration);
            _dbContextTransaction = _bancoContext.Database.BeginTransaction();
        }

        public async Task<RetornoDto> Incluir<T>(T entity) where T : Entity
        {
            try
            {
                await _bancoContext.Set<T>().AddAsync(entity);
                var resultado = await _bancoContext.SaveChangesAsync();

                return new RetornoDto
                {
                    Resultado = resultado > 0
                };
            }
            catch (Exception)
            {
                _dbContextTransaction.Rollback();
                throw;
            }
        }

        public virtual async Task<RetornoDto> Gravar()
        {
            try
            {
                var resultado = await _bancoContext.SaveChangesAsync();

                return new RetornoDto
                {
                    Resultado = resultado > 0
                };
            }
            catch (Exception)
            {
                _dbContextTransaction.Rollback();
                throw;
            }
        }

        public async Task<RetornoDto> Excluir<T>(Guid guid) where T : Entity
        {
            try
            {
                var item = await _bancoContext.Set<T>()
                                              .AsNoTracking()
                                              .SingleOrDefaultAsync(x => x.Guid == guid);

                _bancoContext.Set<T>().Remove(item);
                var resultado = await _bancoContext.SaveChangesAsync();

                return new RetornoDto
                {
                    Resultado = resultado > 0
                };
            }
            catch (Exception)
            {
                _dbContextTransaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<T>> Listar<T>() where T : Entity
        {
            return await _bancoContext.Set<T>()
                                      .AsNoTracking()
                                      .ToListAsync();
        }


        public IEnumerable<T> Listar<T>(Func<T, bool> predicado) where T : Entity
        {
            return _bancoContext.Set<T>()
                                .AsNoTracking()
                                .Where(predicado);
        }

        public async Task<T> Obter<T>(Guid guid) where T : Entity
        {
            return await _bancoContext.Set<T>()
                                      .SingleOrDefaultAsync(x => x.Guid == guid);
        }

        protected override void Commit()
        {
            _dbContextTransaction.Commit();
        }

        protected override void Rollback()
        {
            _dbContextTransaction.Dispose();
        }
    }
}
