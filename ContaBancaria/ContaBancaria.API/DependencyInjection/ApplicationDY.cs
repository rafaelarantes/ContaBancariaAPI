using ContaBancaria.Application;
using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ContaBancaria.API.DependencyInjection
{
    public static class ApplicationDY
    {
        public static void StartApplication(this IServiceCollection services)
        {
            services.AddSingleton<IRetornoMapper, RetornoMapper>();
            services.AddSingleton<IContaMapper, ContaMapper>();
            services.AddSingleton<IBancoMapper, BancoMapper>();

            services.AddTransient<IAutenticacaoApplication, AutenticacaoApplication>();
            services.AddTransient<IContaApplication, ContaApplication>();
            services.AddTransient<IBancoApplication, BancoApplication>();
            services.AddTransient<IBancoCentralApplication, BancoCentralApplication>();
        }
    }
}
