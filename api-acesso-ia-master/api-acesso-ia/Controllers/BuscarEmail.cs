using api_acesso_ia.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_acesso_ia.Controllers
{
    public class BuscarEmail
    {

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Usuario>> BuscarEmail(int id)
        {
            var usuario = await _usuarioService.BuscarPorIdService(id);
            if (usuario == null)
            {
                return NotFound(new { msg = "Usuário não encontrado" });
            }
            return Ok(usuario);
        }
    }
}
