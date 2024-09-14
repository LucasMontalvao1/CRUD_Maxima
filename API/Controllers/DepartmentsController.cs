using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var departments = await _departmentService.GetAllAsync();
                if (departments == null || !departments.Any())
                {
                    return NotFound("Nenhum departamento encontrado.");
                }

                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao recuperar departamentos.");
            }
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            try
            {
                var department = await _departmentService.GetByCodigoAsync(codigo);
                if (department == null)
                {
                    return NotFound("Departamento não encontrado.");
                }

                return Ok(department);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao recuperar departamento.");
            }
        }
    }
}
