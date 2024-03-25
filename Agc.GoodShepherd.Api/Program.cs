using Agc.GoodShepherd.Api;
using Agc.GoodShepherd.Infrastructure.DataAccess;
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
    if (ctx.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        ctx.Database.Migrate();
}

app.ConfigureMiddleware();
//
app.Run();