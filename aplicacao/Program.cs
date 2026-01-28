using KomercioApi.Data;
using KomercioApi.Interface; 
using KomercioApi.Repository;
using KomercioApi.Service;
using KomercioApi.Service;  
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi(); 
builder.Services.AddAuthorization();

// Configuração do Banco

var connStr = builder.Configuration.GetConnectionString("ApiBaseUrl");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(connStr)
           .UseSnakeCaseNamingConvention();
});


// Lista Compras
builder.Services.AddScoped<IItensListaComprasRepository, ListaComprasRepository>(); 
builder.Services.AddScoped<IListaComprasService, ListaComprasService>();

// Itens Lista Compras
builder.Services.AddScoped<IListaCompraRepository, ItensListaComprasRepository>();
builder.Services.AddScoped<IItensListaComprasService, ItensListaComprasService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();