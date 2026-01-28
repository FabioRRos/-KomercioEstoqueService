using KomercioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KomercioApi.Data
{
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)    
    {
        public DbSet<ItensListaComprasDTO> listadecompras { get; set; }
        public DbSet<ListaComprasDTO> listas { get; set; }
    }
}