using Microsoft.EntityFrameworkCore;
using TALLERMECANICO.CORE.Core.Interfaces;
using TALLERMECANICO.CORE.Core.Services;
using TALLERMECANICO.CORE.Infrastructure.Data;
using TALLERMECANICO.CORE.Infrastructure.Repositories;
using TALLERMECANICO.CORE.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: false);

var connectionString = builder.Configuration.GetConnectionString("TallerMecanicoDb")
    ?? throw new InvalidOperationException("Connection string 'TallerMecanicoDb' was not found.");

builder.Services.AddDbContext<TallerMecanicoDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ITallerMecanicoService, TallerMecanicoService>();
builder.Services.AddScoped<IOrdenServicioRepository, OrdenServicioRepository>();
builder.Services.AddScoped<IOrdenServicioService, OrdenServicioService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
