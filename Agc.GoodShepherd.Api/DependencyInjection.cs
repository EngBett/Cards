using System.Text.Json.Serialization;
using Agc.GoodShepherd.Api.Filters;
using Agc.GoodShepherd.Api.Services;
using Agc.GoodShepherd.Application;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Domain.Models;
using Agc.GoodShepherd.Infrastructure;
using Agc.GoodShepherd.Infrastructure.DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Agc.GoodShepherd.Api;

public static class DependencyInjection
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    { 
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);//

        services.AddControllers(opt => { opt.Filters.Add(typeof(GlobalExceptionFilter)); }).AddFluentValidation()
            .AddJsonOptions(o => o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
        ////

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
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddBearerToken();
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddApplicationDependencies();
        services.AddInfrastructureDependencies();

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

    public static void ConfigureMiddleware(this WebApplication app)
    {
        app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseSwagger();
        app.UseSwaggerUI();

        //app.UseHttpsRedirection();
        
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapIdentityApi<ApplicationUser>();
        app.MapControllers();
    }
}