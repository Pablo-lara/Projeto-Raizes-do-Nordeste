using Microsoft.EntityFrameworkCore;
using ProjetoRaizes.Data;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarPedidoAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido?> BuscarPorIdAsync(int id)
        {
            return await _context.Pedidos.Include(p => p.Itens).ThenInclude(i => i.ItemCardapio).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> CriarPedidoAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }
    }
}
