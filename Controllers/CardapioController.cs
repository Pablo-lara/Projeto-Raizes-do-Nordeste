using Microsoft.AspNetCore.Mvc;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Filters;

namespace ProjetoRaizes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardapioController : ControllerBase
    {
        private readonly IItemCardapioService _cardapioService;

        public CardapioController(IItemCardapioService cardapioService)
        {
            _cardapioService = cardapioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterCardapio()
        {
            var itens = await _cardapioService.ListarMenuDisponivelAsync();
            return Ok(itens);
        }
    }
}
