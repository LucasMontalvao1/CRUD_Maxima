using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department> GetByCodigoAsync(string codigo);

        Task<bool> ExistsAsync(string codigo);
    }
}
