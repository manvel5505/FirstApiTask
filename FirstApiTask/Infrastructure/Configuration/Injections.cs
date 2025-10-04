using AutoMapper;
using FirstApiTask.Domain.interfaces;
using FirstApiTask.Infrastructure.Data;
using FirstApiTask.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FirstApiTask.Infrastructure.Configuration
{
    public static class Injections
    {
        public static IServiceCollection AddDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString(nameof(StorageDbContext));

            if (connection == null)
            {
                throw new ArgumentException(nameof(connection));
            }
            
            services.AddDbContext<StorageDbContext>(opt => opt.UseSqlServer(connection));

            services.AddScoped<ICombinationService, CombinationService>();
            services.AddScoped <ICombinationGenerator, CombinationGeneration>();
            services.AddScoped<ICombinationBuilder, CombinationGeneration>();
            
            return services;
        }
    }
}
