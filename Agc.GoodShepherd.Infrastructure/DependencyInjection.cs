using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Agc.GoodShepherd.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IRepository, Repository>();
        return services;
    }
}