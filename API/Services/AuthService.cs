using API.Models;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User> ValidarUsuarioAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("O nome de usuário não pode estar vazio.", nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("A senha não pode estar vazia.", nameof(password));

            try
            {
                return await _authRepository.ValidarUsuarioAsync(username, password);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao validar o usuário.", ex);
            }
        }
    }
}
