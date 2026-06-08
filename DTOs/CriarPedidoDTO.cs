namespace ProjetoRaizes.DTOs
{
    public class CriarPedidoDTO
    {
        public int UsuarioId { get; set; }
        public List<ItemCarrinhoDTO> Itens { get; set; } = new();
    }

    public class ItemCarrinhoDTO
    {
        public int ItemCardapioId {  get; set; }
        public int Quantidade { get; set; }
    }
}
