using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult GetAll()
        {
            var departments = _departmentService.GetAll();

            if (departments == null || !departments.Any())
                return NotFound("Nenhum departamento encontrado.");

            return Ok(departments);
        }


        [HttpGet("codigo/{codigo}")]
        public IActionResult GetByCodigo(string codigo)
        {
            try
            {
                var department = _departmentService.GetByCodigo(codigo);
                if (department == null)
                    return NotFound("Departamento não encontrado.");

                return Ok(department);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
