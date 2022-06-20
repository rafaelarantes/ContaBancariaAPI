using ContaBancaria.Data.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContaBancaria.Cross.Mediator
{
    public abstract class Handler : IHandler
    {
        public Guid Guid { get; private set; }

        public Handler()
        {
            Guid = Guid.NewGuid();
        }

        public async Task Execute(CancellationToken cancelationToken)
        {
            await ExecuteMethod(cancelationToken);
        }

        protected abstract Task<RetornoDto> ExecuteMethod(CancellationToken cancelationToken, ServiceBus ser);
    }
}
