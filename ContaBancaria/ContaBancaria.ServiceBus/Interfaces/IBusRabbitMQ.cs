using System.Net.Http;
using System.Threading.Tasks;

namespace ContaBancaria.ServiceBus.Interfaces
{
    public interface IBusRabbitMQ
    {
        void Executar();

        void CriarClient(HttpClient httpClient);
    }
}
