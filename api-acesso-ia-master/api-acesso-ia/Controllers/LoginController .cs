using api_acesso_ia.Models;
using api_acesso_ia.Request;
using api_acesso_ia.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_acesso_ia.Controllers
{
    [Route("api/v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Autenticar([FromBody] LoginRequest dados)
        {
            var usuario = await _loginService.AutenticarService(dados.Login, dados.Senha);
            if (usuario == null)
            {
                return Unauthorized(new { msg = "Login ou Senha inválidos!" });
            }
            return Ok(new { Nome = usuario.Nome, Email = usuario.Email, Login = usuario.Login });
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<LoginUsuario>> Salvar([FromBody] LoginUsuario dados)
        {
            if (await _loginService.CpfJaCadastradoService(dados.Cpf))
            {
                throw new Exception("O CPF informado já possui cadastro.");
            }

            dados.Senha = _loginService.CriptografarSenha(dados.Senha);
            var usuario = await _loginService.CadastrarService(dados);
            return CreatedAtAction(nameof(Salvar), new { id = dados.Id }, dados);
        }

        [HttpGet("buscar-email")]
        public async Task<IActionResult> BuscarEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { msg = "E-mail não pode ser vazio." });
            }

            var usuario = await _loginService.BuscarEmail(email);

            if (usuario == null)
            {
                return NotFound(new { msg = "Usuário não encontrado." });
            }

            return Ok(usuario);
        }


        [HttpPut("resetar-senha/{id}")]
        public async Task<IActionResult> RedefinirSenha(int id, [FromBody] RedefinirSenhaRequest request)
        {
            var sucesso = await _loginService.RedefinirSenha(id, request.NovaSenha);
            if (!sucesso)
            {
                return StatusCode(500, new { msg = "Erro ao redefinir senha." });
            }

            return Ok(new { msg = "Senha redefinida com sucesso." });
        }
    }
}
