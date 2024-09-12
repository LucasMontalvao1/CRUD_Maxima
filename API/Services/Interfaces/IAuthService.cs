using API.Models;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        User ValidarUsuario(string username, string password);
    }
}
