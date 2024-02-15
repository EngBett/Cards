using Cards.Application.Interfaces;
using Cards.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IRepository, Repository>();
        return services;
    }
}