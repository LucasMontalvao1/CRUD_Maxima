using API.Models;
using System.Threading.Tasks;

namespace API.Repositorys.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> ValidarUsuarioAsync(string username, string password);
    }
}
