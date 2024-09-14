using API.Models.DTO;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Dados de login não podem ser nulos.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuario = await _authService.ValidarUsuarioAsync(userDto.Username, userDto.Password);
                if (usuario != null)
                {
                    return Ok(new
                    {
                        User = new
                        {
                            usuario.id_usuario,
                            usuario.Username,
                            usuario.Name,
                            usuario.Email
                        }
                    });
                }
                return Unauthorized("Login ou senha incorretos");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao processar a solicitação de login.");
            }
        }
    }
}
