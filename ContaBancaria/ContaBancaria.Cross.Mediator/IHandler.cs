using ContaBancaria.Data.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace ContaBancaria.Cross.Mediator
{
    public interface IHandler
    {
        Task Execute(CancellationToken cancelationToken);
    }
}
