using API.Models;
using System.Collections.Generic;

namespace API.Repositorys.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();

        Department GetByCodigo(string codigo);
    }
}
