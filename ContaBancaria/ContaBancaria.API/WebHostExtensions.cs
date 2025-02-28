using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ContaBancaria.API
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDatabase<T>(this IWebHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<T>();

                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return webHost;
        }
    }
}
