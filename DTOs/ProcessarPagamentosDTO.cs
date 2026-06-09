using ProjetoRaizes.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjetoRaizes.DTOs
{
    public class ProcessarPagamentosDTO
    {
        public int PedidoId {  get; set; }
        
        public FormaPagamento FormaPagamento { get; set; }
    }
}
