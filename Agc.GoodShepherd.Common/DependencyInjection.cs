using System.Text.Json;
using Agc.GoodShepherd.Common.Options;
using Agc.GoodShepherd.Common.RestServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Agc.GoodShepherd.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonDependencies(this IServiceCollection services)
    {
        
        return services;
    }
}