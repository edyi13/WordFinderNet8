using Microsoft.Extensions.DependencyInjection;
using WordFinder.Application.Interface.Persistence;
using WordFinder.Infrastructure.Persistence.Repositories;

namespace WordFinder.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddScoped<IWordFinderRepository, WordFinderRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
