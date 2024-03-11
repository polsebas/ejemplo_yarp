using MediatR;
using Microsoft.EntityFrameworkCore;
using ApiUsuario.Usuarios.infrastructure;
using ApiUsuario.Usuarios.application;
using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
//using RabbitMQ.Bus.BusRabbit;
//using RabbitMQ.Bus.EventoQueue;
//using ApiUsuario.ManejadorRabbit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IEventoManejador<EmailEventoQueue>, EmailEventoManejador>();
//builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssemblyContaining<Nuevo>());
builder.WebHost.UseKestrel(kestrelOptions =>
{
    kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});
builder.Services.AddDbContext<ContextoUsuario>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
