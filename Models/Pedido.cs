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


    public enum CanalPedido
    {
        App = 1,
        Balcao = 2,
        Totem = 3
    }

    public enum FormaPagamento
    {
        Credito = 1,
        Debito = 2,
        Dinheiro = 3
    }

    public class Pedido 
    {
        [Key]
        public int Id { get; set; } 

        public int UsuarioId { get; set; }

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        public decimal ValorTotal  { get; set; }

        public StatusPedido Status { get; set; } = StatusPedido.Recebido;

        public CanalPedido Canal {  get; set; }

        public List<ItemPedido> Itens { get; set; } = new();
    }

}
