using ProjetoRaizes.DTOs;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemCardapioRepository _cardapioRepository;
        
        public PedidoService(IPedidoRepository pedidoRepository, IItemCardapioRepository cardapioRepository)
        {
            _cardapioRepository = cardapioRepository;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido?> AtualizarStatusAsync(int id, StatusPedido novoStatus)
        {
            var pedido = await _pedidoRepository.BuscarPorIdAsync(id);
            if (pedido == null) return null;

            pedido.Status = novoStatus;
            await _pedidoRepository.AtualizarPedidoAsync(pedido);
            return pedido;
        }

        public async Task<Pedido?> ConsultarStatusAsync(int id)
        {
            return await _pedidoRepository.BuscarPorIdAsync(id);
        }

        public async Task<string> ProcessarPagamentoSimuladoAsync(ProcessarPagamentosDTO dto)
        {
            var pedido = await _pedidoRepository.BuscarPorIdAsync(dto.PedidoId);

            if(pedido == null)
            {
                throw new Exception("Pedido não localizado");
            }

            if(pedido.Status != StatusPedido.Recebido)
            {
                throw new Exception($"Não é possível pagar um pedido com o status atual: {pedido.Status}.");
            }

            decimal valorCobrado = pedido.ValorTotal;
            string formatoPagamentoTexto = dto.FormaPagamento.ToString();

            pedido.Status = StatusPedido.EmPreparacao;
            await _pedidoRepository.AtualizarPedidoAsync(pedido);

            return $"Pagamento de R$ {valorCobrado:N2} aprovado via {formatoPagamentoTexto}. O pedido está agora Em Preparação!";
        }

        public async Task<Pedido> RealizarPedidoAsync(CriarPedidoDTO dto)
        {
            var novoPedido = new Pedido { UsuarioId = dto.UsuarioId, Canal = dto.Canal};
            decimal valorTotalGeral = 0;

            var cardapio = await _cardapioRepository.ObterTodosAtivosAsync();

            foreach(var itemDto in dto.Itens)
            {
                var produtoCardapio = cardapio.FirstOrDefault(c => c.Id == itemDto.ItemCardapioId);
                if (produtoCardapio == null) throw new Exception($"Item {itemDto.ItemCardapioId} não encontrado no cardápio.");

                var itemPedido = new ItemPedido
                {
                    ItemCardapioId = itemDto.ItemCardapioId,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produtoCardapio.Preco
                };

                valorTotalGeral += itemPedido.PrecoUnitario * itemPedido.Quantidade;
                novoPedido.Itens.Add(itemPedido);
            }

            novoPedido.ValorTotal = valorTotalGeral;
            return await _pedidoRepository.CriarPedidoAsync(novoPedido);
        }
    }
}
