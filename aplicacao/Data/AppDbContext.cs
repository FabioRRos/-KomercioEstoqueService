using Microsoft.EntityFrameworkCore;

namespace KomercioApi.Data
{
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)    
    {
        public DbSet<ListaComprasDTO> listadecompras { get; set; }

    }
}