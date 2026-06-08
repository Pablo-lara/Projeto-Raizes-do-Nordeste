using Microsoft.EntityFrameworkCore;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<ItemCardapio> ItensCardapio { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ItemPedido> ItensPedidos { get; set; }
    }
}
