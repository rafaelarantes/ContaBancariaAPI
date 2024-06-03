using System.Net.Http;
using System.Threading.Tasks;

namespace ContaBancaria.ServiceBus.Interfaces
{
    public interface IBusDb
    {
       Task Executar();

        void CriarClient(HttpClient httpClient);
    }
}
