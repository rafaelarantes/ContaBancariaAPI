using ContaBancaria.Data.Contexts;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public abstract class Repository : IDisposable
    {
        protected BancoContext _bancoContext;

        private IDbContextTransaction _dbContextTransaction;
        private bool disposedValue;

        public Repository(IConfiguration configuration)
        {
            _bancoContext = new BancoContext(configuration);
            _dbContextTransaction = _bancoContext.Database.BeginTransaction();
        }

        public async Task<RetornoDto> Incluir<T>(T entity) where T : Entity
        {
            try
            {
                await _bancoContext.AddAsync(entity);
                _bancoContext.SaveChanges();

                return new RetornoDto
                {
                    Resultado = true
                };
            }
            catch (Exception)
            {
                _dbContextTransaction.Rollback();
                throw;
            }
        }

        public async Task<RetornoDto> Atualizar<T>(T entity) where T : Entity
        {
            try
            {
                await Task.Run(() => _bancoContext.Set<T>().Update(entity));
                _bancoContext.SaveChanges();

                return new RetornoDto
                {
                    Resultado = true
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
                _bancoContext.SaveChanges();

                return new RetornoDto
                {
                    Resultado = true
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

        public async Task<T> Obter<T>(Guid guid) where T : Entity
        {
            return await _bancoContext.Query<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Guid == guid);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _bancoContext.SaveChanges();
                    _dbContextTransaction.Commit();
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(disposing: false);
        }
    }
}
