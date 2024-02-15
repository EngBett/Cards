using Cards.Api;
using Cards.Domain.Models;
using Cards.Infrastructure.DataAccess;
using Cards.Infrastructure.DataAccess.DataSeed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureServices(builder.Configuration);
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://host.docker.internal:5341"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (ctx.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        ctx.Database.Migrate();

    await userManager.SeedUsers(roleManager);
}

app.ConfigureMiddleware();
app.Run();