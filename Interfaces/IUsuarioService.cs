using ProjetoRaizes.DTOs;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Interfaces
{
    public interface IUsuarioService
    {
        Task<string> CriptoSenhaAsync(string senha);
        Task<bool> verificacaoSenhaAsync(string senha, string senhaHash);
        Task<string> VerificarPermissaoAsync();
        Task<bool> GravarInfoPermissaoAsync(int usuarioId, bool aceitou);
        Task<Usuario> DefinirDataAsync(Usuario usuario);

        Task<Usuario> LoginUsuarioAsync(LoginDTO login);
    }
}
