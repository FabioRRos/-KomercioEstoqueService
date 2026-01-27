using Microsoft.EntityFrameworkCore;
using KomercioApi.Data;
using KomercioApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Registro de Serviços
builder.Services.AddControllers(); 
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

// Configuração do Banco
var connStr = builder.Configuration.GetConnectionString("ApiBaseUrl");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(connStr)
           .UseSnakeCaseNamingConvention(); 
});


builder.Services.AddScoped<ListaCompraRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); 

app.Run();