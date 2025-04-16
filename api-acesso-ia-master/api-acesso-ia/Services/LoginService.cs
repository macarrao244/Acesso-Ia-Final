using api_acesso_ia.Models;
using api_acesso_ia.Repositories.Interfaces;
using api_acesso_ia.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace api_acesso_ia.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<LoginUsuario> AutenticarService(string login, string senha)
        {
            var senhaHash = CriptografarSenha(senha);
            return await _loginRepository.Autenticar(login, senhaHash);
        }

        public async Task<LoginUsuario> CadastrarService(LoginUsuario dados)
        {
            return await _loginRepository.Cadastrar(dados);
        }

        public async Task<bool> CpfJaCadastradoService(string cpf)
        {
            return await _loginRepository.CpfJaCadastrado(cpf);
        }

        public string CriptografarSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var cript = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(cript);
            }
        }

        public async Task<Usuario> BuscarEmail(string email)
        {
            email = email.Trim().ToLower(); 
            return await _loginRepository.BuscarEmail(email);
        }

        public async Task<bool> RedefinirSenha(int id, string novaSenha)
        {
            var senha = CriptografarSenha(novaSenha);
            return await _loginRepository.AtualizarSenha(id, senha);
        }
    }
}
