using API.Models;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;
using System.Collections.Generic;

namespace API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department GetByCodigo(string codigo)
        {
            return _departmentRepository.GetByCodigo(codigo);
        }
    }
}
