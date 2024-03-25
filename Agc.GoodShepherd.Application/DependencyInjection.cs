using System.Reflection;
using Agc.GoodShepherd.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PhoneNumbers;

namespace Agc.GoodShepherd.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddHttpClient("ekyc", c => { });
            services.AddSingleton<PhoneNumberUtil>(_ => PhoneNumberUtil.GetInstance());

            return services;
        }
    }
}