using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContaBancaria.API.DependencyInjection
{
    public static class DataDY
    {
        public static void StartData(this IServiceCollection services)
        {
            services.AddTransient<IBancoRepository, BancoRepository>();
        }
    }
}
