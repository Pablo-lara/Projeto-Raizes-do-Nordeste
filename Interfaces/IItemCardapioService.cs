using ProjetoRaizes.Models;

namespace ProjetoRaizes.Interfaces
{
    public interface IItemCardapioService
    {
        Task<IEnumerable<ItemCardapio>> ListarMenuDisponivelAsync();
    }
}