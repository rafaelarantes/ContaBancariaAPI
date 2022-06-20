using ContaBancaria.Bus;
using System.Threading.Tasks;

namespace ContaBancara.ServiceBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new Bus().Executar();
        }
    }
}
