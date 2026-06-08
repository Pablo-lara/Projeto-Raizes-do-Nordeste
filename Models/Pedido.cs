using System.ComponentModel.DataAnnotations;

namespace ProjetoRaizes.Models
{
    public enum StatusPedido
    {
        Recebido = 1,
        EmPreparacao = 2,
        Pronto = 3,
        Entregue = 4
    }

    public class Pedido 
    {
        [Key]
        public int Id { get; set; } 

        public int UsuarioId { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        public decimal ValorTotal  { get; set; }

        public StatusPedido Status { get; set; } = StatusPedido.Recebido;

        public List<ItemPedido> Itens { get; set; } = new();
    }

}
