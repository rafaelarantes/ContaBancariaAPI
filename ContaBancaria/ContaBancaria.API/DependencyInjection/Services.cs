using ContaBancaria.Application;
using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ContaBancaria.API
{
    public static class Services
    {
        public static void Start(this IServiceCollection services)
        {
            services.AddSingleton<IContaMapper, ContaMapper>();
            services.AddSingleton<IBancoMapper, BancoMapper>();

            services.AddTransient<IContaApplication, ContaApplication>();
            services.AddTransient<IBancoApplication, BancoApplication>();
        }
    }
}