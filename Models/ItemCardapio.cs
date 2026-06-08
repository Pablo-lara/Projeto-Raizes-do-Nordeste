using System.ComponentModel.DataAnnotations;

namespace ProjetoRaizes.Models
{
    public class ItemCardapio
    {
        [Key]
        public int Id  { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(225)]
        public string Descricao { get; set; }

        [Required]
        public decimal Preco {  get; set; }

        public bool Ativo { get; set; } = true; //permite remover algum prato do cardapio caso seja necessario

    }
}
