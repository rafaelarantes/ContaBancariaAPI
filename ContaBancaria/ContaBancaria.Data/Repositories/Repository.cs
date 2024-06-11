using System;

namespace ContaBancaria.Data.Repositories
{
    public abstract class Repository : IDisposable
    {
        private bool disposedValue;

        protected abstract void Disposing();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disposing();
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
