using API.Models;

namespace API.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();

        Task<Department> GetByCodigoAsync(string codigo);

        Task<bool> ExistsAsync(string codigo);
    }
}
