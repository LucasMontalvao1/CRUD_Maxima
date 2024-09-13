using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositorys.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department> GetByCodigoAsync(string codigo);

        Task<bool> ExistsAsync(string codigo);
    }
}
