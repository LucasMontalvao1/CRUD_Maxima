using API.Models;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthService> _logger; // Adicionando Logger

        public AuthService(IAuthRepository authRepository, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _logger = logger;
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
                _logger.LogError(ex, "Erro ao validar o usuário com username: {username}", username); 
                throw new ApplicationException("Erro ao validar o usuário.", ex);
            }
        }
    }
}
