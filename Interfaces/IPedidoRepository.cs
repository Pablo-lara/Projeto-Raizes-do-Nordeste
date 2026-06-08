using ProjetoRaizes.Models;

namespace ProjetoRaizes.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> CriarPedidoAsync(Pedido pedido);
        Task<Pedido?> BuscarPorIdAsync(int id);
        Task AtualizarPedidoAsync(Pedido pedido);
    }
}
