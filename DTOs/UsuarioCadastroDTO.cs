namespace ProjetoRaizes.DTOs
{
    public class UsuarioCadastroDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool PermissaoDados { get; set; }

        public DateTime DataPermissao {  get; set; }
    }
}
