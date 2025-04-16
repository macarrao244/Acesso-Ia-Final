using api_acesso_ia.Models;
using api_acesso_ia.Dtos;
using api_acesso_ia.Repositories.Interfaces;
using api_acesso_ia.Services.Interfaces;

namespace api_acesso_ia.Services
{
    public class AcessoService : IAcessoService
    {
        private readonly IAcessoRepository _acessoRepository;

        public AcessoService(IAcessoRepository acessoRepository)
        {
            _acessoRepository = acessoRepository;
        }

        public Task<IEnumerable<AcessoResponse>> ListarTodos()
        {
            return _acessoRepository.ListarTodos();
        }

        public async Task<bool> Registrar(Acesso acesso)
        {
            var usuarioExiste = await _acessoRepository.UsuarioExiste(acesso.IdUsuario);
            if (!usuarioExiste)
                return false;

            return await _acessoRepository.Registrar(acesso);
        }
    }
}
