using Microsoft.EntityFrameworkCore;
using ProjetoRaizes.Data;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Repositories
{
    public class ItemCardapioRepository : IItemCardapioRepository
    {
        private readonly AppDbContext _context;
        public ItemCardapioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemCardapio>> ObterTodosAtivosAsync()
        {
            return await _context.ItensCardapio.Where(item => item.Ativo).ToListAsync();
        }
    }
}
