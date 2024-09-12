using API.Models.DTO;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Login([FromBody] UserDto userDto)
        {
            var usuario = _authService.ValidarUsuario(userDto.Username, userDto.Password);
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
    }
}
