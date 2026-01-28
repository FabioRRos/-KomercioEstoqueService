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



// Itens Lista Compras
builder.Services.AddScoped<IListaCompraRepository, ItensListaComprasRepository>();
builder.Services.AddScoped<IItensListaComprasService, ItensListaComprasService>();

// Lista Compras
builder.Services.AddScoped<IItensListaComprasRepository, ListaComprasRepository>(); 
builder.Services.AddScoped<IListaComprasService, ListaComprasService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Cria o arquivo .json
    app.MapScalarApiReference(); // <--- CRIA A TELA VISUAL EM /scalar/v1
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();