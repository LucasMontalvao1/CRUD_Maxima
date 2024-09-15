using API.Models.DTO;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
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

                return Unauthorized("Login ou senha incorretos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar logar o usuário com username: {username}", userDto.Username);
                return StatusCode(500, "Dados de login incorretos.");
            }
        }
    }
}
