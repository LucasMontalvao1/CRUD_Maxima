using API.Models;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public User ValidarUsuario(string username, string password)
        {
            return _authRepository.ValidarUsuario(username, password);
        }
    }
}
