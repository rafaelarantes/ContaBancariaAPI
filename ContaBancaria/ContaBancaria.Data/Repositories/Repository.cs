using System;

namespace ContaBancaria.Data.Repositories
{
    public abstract class Repository : IDisposable
    {
        private bool disposedValue;

        protected abstract void Commit();
        protected abstract void Rollback();


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try
                    {
                        Commit();
                    }
                    catch (Exception)
                    {
                        Rollback();
                        throw;
                    }
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
