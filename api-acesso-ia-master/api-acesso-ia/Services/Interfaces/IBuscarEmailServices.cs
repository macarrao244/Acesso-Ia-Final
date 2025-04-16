using api_acesso_ia.Models;

namespace api_acesso_ia.Services.Interfaces
{
    public interface IBuscarEmailServices
    {
        Task<Usuario> BuscarEmail(string email);
    }
}
