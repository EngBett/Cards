using System.Text.Json.Serialization;
using Agc.GoodShepherd.Application;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Domain.Models;
using Agc.GoodShepherd.Infrastructure;
using Agc.GoodShepherd.Infrastructure.DataAccess;
using Agc.GoodShepherd.Ui.Components.Account;
using Agc.GoodShepherd.Ui.Filters;
using Agc.GoodShepherd.Ui.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Ui;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.AddControllers(opt => { opt.Filters.Add(typeof(GlobalExceptionFilter)); }).AddFluentValidation()
            .AddJsonOptions(o => o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

        services.AddHttpContextAccessor();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(config["DATABASE_CONNECTION"]); });
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();
        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        
        services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddApplicationDependencies();
        services.AddInfrastructureDependencies();

        services.AddEndpointsApiExplorer();
    }

    public static void ConfigureMiddleware(this WebApplication app)
    {
    }
}