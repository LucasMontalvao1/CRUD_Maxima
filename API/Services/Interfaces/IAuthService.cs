using API.Models;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> ValidarUsuarioAsync(string username, string password);
    }
}
