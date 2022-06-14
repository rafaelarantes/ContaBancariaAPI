using ContaBancaria.Application;
using ContaBancaria.Application.Contratos;
using Microsoft.Extensions.DependencyInjection;

namespace ContaBancaria.API
{
    public static class Services
    {
        public static void Start(this IServiceCollection services)
        {
            services.AddTransient<IContaApplication, ContaApplication>();
        }
    }
}