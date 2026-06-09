using Microsoft.AspNetCore.Mvc;
using ProjetoRaizes.Interfaces;
using ProjetoRaizes.Models;
using ProjetoRaizes.Filters;
using ProjetoRaizes.DTOs;

namespace ProjetoRaizes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioService usuarioService, IUsuarioRepository usuarioRepository)
        {
            _usuarioService = usuarioService;
            _usuarioRepository = usuarioRepository;
        }


        [HttpGet("termo-consentimento")]
        public async Task<IActionResult> ObterConsentimentoDados()
        {
            var textoTermo = await _usuarioService.VerificarPermissaoAsync();

            return Ok(new { texto = textoTermo });
        }


        [HttpPost("register")]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            /*
            if (!usuario.PermissaoDados)
            {
                return BadRequest("É necessário consentir com o uso de dados para se cadastrar");
            }
            */

            usuario.SenhaHash = await _usuarioService.CriptoSenhaAsync(usuario.SenhaHash);
            usuario = await _usuarioService.DefinirDataAsync(usuario);

            var usuarioSalvo = await _usuarioRepository.SalvarUsuarioAsync(usuario);

            return Ok(new
            {
                mensagem = "Cadastro realizado com sucesso",
                id = usuario.Id,
                nome = usuario.Nome,
                sobrenome = usuario.Sobrenome,
                email = usuario.Email
            });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var usuarioAutenticado = await _usuarioService.LoginUsuarioAsync(loginDTO);

            if(usuarioAutenticado == null)
            {
                return Unauthorized(new { mensagem = "Email ou senha incorretos." });
            }

            return Ok(new
            {
                mensagem = "Login realizado com sucesso!",
                usuarioId = usuarioAutenticado.Id,
                nome = usuarioAutenticado.Nome,
                aceitouTermos = usuarioAutenticado.PermissaoDados
            });
        }
    }
}
