using System.Text.Json;
using System.Text.Json.Serialization;
using Agc.GoodShepherd.Application;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Options;
using Agc.GoodShepherd.Common.RestServices;
using Agc.GoodShepherd.Domain.Models;
using Agc.GoodShepherd.Infrastructure;
using Agc.GoodShepherd.Infrastructure.DataAccess;
using Agc.GoodShepherd.Mvc.Filters;
using Agc.GoodShepherd.Mvc.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Refit;

namespace Agc.GoodShepherd.Mvc;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        services.AddRazorPages();
        services.AddControllersWithViews();
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

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager()
            .AddApiEndpoints()
            .AddDefaultTokenProviders();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddApplicationOptions(config);
        services.AddRefitClients(config);
        services.AddApplicationDependencies();
        services.AddInfrastructureDependencies();

        services.AddSingleton<Random>();

        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AGC GoodShepherd Church", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
    
    public static void AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var mpesaOptions = configuration.GetSection(nameof(MpesaOptions)).Get<MpesaOptions>();

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })
        };

        services.AddRefitClient<IPochipayServices>(_ => refitSettings)
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(mpesaOptions.BaseUrl);
                c.DefaultRequestHeaders.Add("X-Api-Key", mpesaOptions.ApiKey);
            });
    }
    
    public static void AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MpesaOptions>(configuration.GetSection(nameof(MpesaOptions)));
    }
}