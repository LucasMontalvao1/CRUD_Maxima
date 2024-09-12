using API.Models;

namespace API.Repositorys.Interfaces
{
    public interface IAuthRepository
    {
        User ValidarUsuario(string username, string password);
    }
}
