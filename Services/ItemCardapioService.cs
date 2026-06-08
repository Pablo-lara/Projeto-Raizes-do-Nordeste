using ProjetoRaizes.Interfaces;

using ProjetoRaizes.Models;

namespace ProjetoRaizes.Services
{
    public class ItemCardapioService : IItemCardapioService
    {
        private readonly IItemCardapioRepository _cardapioRepository;
        public ItemCardapioService(IItemCardapioRepository cardapioRepository)
        {
            _cardapioRepository = cardapioRepository;
        }
        public async Task<IEnumerable<ItemCardapio>> ListarMenuDisponivelAsync()
        {
            return await _cardapioRepository.ObterTodosAtivosAsync();
        }
    }
}
