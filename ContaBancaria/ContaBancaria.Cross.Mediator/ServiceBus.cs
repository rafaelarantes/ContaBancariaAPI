using ContaBancaria.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContaBancaria.Cross.Mediator
{
    public class ServiceBus : IServiceBus
    {
        private readonly Queue<IHandler> _handlers;
        private readonly Queue<IHandler> _failedHandlers;

        public async Task<RetornoDto> Add(IHandler handler)
        {
            _handlers.Enqueue(handler);

            return new RetornoDto
            {
                Resultado = true
            };
        }

        public async Task Execute()
        {
            var cancelationToken = CancellationToken.None;

            while (!cancelationToken.IsCancellationRequested)
            {
                foreach (var handler in _handlers)
                {
                    try
                    {
                        await handler.Execute(cancelationToken);
                    }
                    catch (Exception)
                    {
                        _failedHandlers.Enqueue(handler);
                    }
                }

                foreach (var failedHandler in _failedHandlers)
                {
                    _handlers.Enqueue(failedHandler);
                }
            }
        }

    }
}
