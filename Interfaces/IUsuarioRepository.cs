using ProjetoRaizes.Models;

namespace ProjetoRaizes.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> SalvarUsuarioAsync(Usuario usuario);

        Task<Usuario> BuscarPorIdAsync(int id);
        Task<Usuario> AtualizarUsuarioAsync(Usuario usuario);

        Task<Usuario> BuscarPorEmailAsync(string email);

    }
}
