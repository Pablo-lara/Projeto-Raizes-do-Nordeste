using ProjetoRaizes.DTOs;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> RealizarPedidoAsync(CriarPedidoDTO dto);
        Task<Pedido?> ConsultarStatusAsync(int id);
        Task<Pedido?> AtualizarStatusAsync(int id, StatusPedido novoStatus);
    }
}
