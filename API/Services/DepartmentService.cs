using API.Models;
using API.Repositorys.Interfaces;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            try
            {
                return await _departmentRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao obter todos os departamentos.", ex);
            }
        }

        public async Task<Department> GetByCodigoAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("O código do departamento não pode estar vazio.", nameof(codigo));

            try
            {
                var department = await _departmentRepository.GetByCodigoAsync(codigo);
                return department;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao obter o departamento com código {codigo}.", ex);
            }
        }

        public async Task<bool> ExistsAsync(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false; 

            try
            {
                return await _departmentRepository.ExistsAsync(codigo);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao verificar a existência do departamento com código {codigo}.", ex);
            }
        }
    }
}
