using Microsoft.Extensions.DependencyInjection;

namespace ContaBancaria.API.DependencyInjection
{
    public static class Services
    {
        public static void Start(this IServiceCollection services)
        {
            services.StartData();
            services.StartApplication();
        }
    }
}