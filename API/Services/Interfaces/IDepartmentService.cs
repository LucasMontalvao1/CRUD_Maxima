using API.Models;
using System.Collections.Generic;

namespace API.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();

        Department GetByCodigo(string codigo);
    }
}
