using ProjetoRaizes.DTOs;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Models;

namespace ProjetoRaizes.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<string> CriptoSenhaAsync(string senha)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(senha));
        }

        public async Task<Usuario> DefinirDataAsync(Usuario usuario)
        {
            usuario.DataCriacao = DateTime.Now;
            return usuario;
        }

        public async Task<bool> verificacaoSenhaAsync(string senha, string senhaHash)
        {
            return await Task.Run(() => BCrypt.Net.BCrypt.Verify(senha, senhaHash));
        }

        public Task<string> VerificarPermissaoAsync()
        {
            string texto = "Declaro que li e aceito os termos de uso e autorizo o processamento dos meus dados pessoais para processos internos do sistema da Raízes do Nordeste.";
            return Task.FromResult(texto);
        }

        public async Task<bool> GravarInfoPermissaoAsync(int usuarioId, bool aceitou)
        {
            var usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId);
            if(usuario == null)
            {
                return false;
            }

            usuario.PermissaoDados = aceitou;
            usuario.DataPermissao = aceitou ? DateTime.Now : null;

            await _usuarioRepository.AtualizarUsuarioAsync(usuario);

            return true;
        }

        public async Task<Usuario> LoginUsuarioAsync(LoginDTO login)
        {
            var usuario = await _usuarioRepository.BuscarPorEmailAsync(login.Email);

            if(usuario == null)
            {
                return null;
            }

            bool senhaValida = await verificacaoSenhaAsync(login.Senha, usuario.SenhaHash);

            if (!senhaValida)
            {
                return null;
            }


            return usuario;
        }
    }
}
