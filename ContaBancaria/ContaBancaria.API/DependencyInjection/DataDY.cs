using ContaBancaria.Data.Contexts;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContaBancaria.API.DependencyInjection
{
    public static class DataDY
    {
        public static void StartData(this IServiceCollection services)
        {
            services.AddDbContext<BancoContext>();
            services.AddTransient<IFilaProcessamentoQueueRepository, FilaProcessamentoQueueRepository>();
            services.AddTransient<IFilaProcessamentoDbRepository, FilaProcessamentoDbRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IContaRepository, ContaRepository>();
            services.AddTransient<IBancoRepository, BancoRepository>();
        }
    }
}
