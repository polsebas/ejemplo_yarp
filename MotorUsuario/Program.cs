//using Buscador.Motor.Models;
//using IT.Hosting.Chassis.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Motor.Usuario;
using Motor.Usuario.Persistencia;

var host = Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((config) =>
{
    config.AddJsonFile("appsettings.json");
    config.AddEnvironmentVariables();
    config.Build();
})
.ConfigureServices((hostContext, services) =>
{
    // Dependencias
    services.AddDbContext<ContextoUsuario>(options =>
    {
        options.UseSqlServer(hostContext.Configuration.GetConnectionString("SqlServer"));
    });
    //services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
    services.AddMemoryCache();
}).Build();

await host.RunAsync();
