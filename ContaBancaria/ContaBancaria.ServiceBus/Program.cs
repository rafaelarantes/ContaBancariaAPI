using ContaBancaria.Data.Contexts;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Repositories;
using ContaBancaria.ServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ContaBancaria.ServiceBus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", false)
            .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddDbContext<BancoContext>();
            services.AddTransient<IFilaProcessamentoQueueRepository, FilaProcessamentoQueueRepository>();
            services.AddTransient<IFilaProcessamentoDbRepository, FilaProcessamentoDbRepository>();
            services.AddTransient<IBusDb, BusDb>();
            services.AddTransient<IBusRabbitMQ, BusRabbitMQ>();
            services.AddTransient<Bus>();

            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<Bus>().Executar();

            Console.ReadKey();
        }
    }
}
