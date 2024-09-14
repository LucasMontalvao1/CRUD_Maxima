using API.Models;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> ValidarUsuarioAsync(string username, string password);
    }
}
