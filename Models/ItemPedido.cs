using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoRaizes.Models
{
    public class ItemPedido
    {
        [Key]
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public int ItemCardapioId { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnitario { get; set; }

        [JsonIgnore]
        public Pedido? Pedido { get; set; }

        public ItemCardapio? ItemCardapio { get; set; }
    }
}
