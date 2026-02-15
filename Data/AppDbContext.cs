using KomercioApi.Models;
using KomercioApi.Models.Entidade;
using Microsoft.EntityFrameworkCore;

namespace KomercioApi.Data
{
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)    
    {
        public DbSet<ItensListaComprasDTO> listadecompras { get; set; }
        public DbSet<ListaComprasDTO> listas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get;set; }
    }
}