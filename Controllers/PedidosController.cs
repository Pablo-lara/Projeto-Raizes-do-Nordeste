using Microsoft.AspNetCore.Mvc;
using ProjetoRaizes.DTOs;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // 1. Endpoint para realizar o pedido
        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoDTO dto)
        {
            try
            {
                var pedido = await _pedidoService.RealizarPedidoAsync(dto);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // 2. Endpoint para o cliente consultar o status atual
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterStatusPedido(int id)
        {
            var pedido = await _pedidoService.ConsultarStatusAsync(id);
            if (pedido == null) return NotFound(new { erro = "Pedido não encontrado." });

            return Ok(new
            {
                PedidoId = pedido.Id,
                StatusAtual = pedido.Status.ToString(), // Retorna o texto do Enum (ex: "Recebido")
                ValorTotal = pedido.ValorTotal,
                Data = pedido.DataPedido
            });
        }

        // 3. Endpoint para a cozinha atualizar o status (ex: Mudar para Pronto)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusDTO dto)
        {
            var pedido = await _pedidoService.AtualizarStatusAsync(id, dto.NovoStatus);
            if (pedido == null) return NotFound(new { erro = "Pedido não encontrado." });

            return Ok(new { mensagem = $"Status do pedido alterado para {pedido.Status} com sucesso!" });
        }
    }
}