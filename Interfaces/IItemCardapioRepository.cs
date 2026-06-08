using ProjetoRaizes.Models;

namespace ProjetoRaizes.Interfaces
{
    public interface IItemCardapioRepository
    {
        public Task<IEnumerable<ItemCardapio>> ObterTodosAtivosAsync();
    }
}
