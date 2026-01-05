using JobPortal.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result)
                return BadRequest(ApiResponse<string>.Fail("Email zaten kayıtlı"));

            return Ok(ApiResponse<string>.Ok(null, "Kayıt başarılı"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized(ApiResponse<string>.Fail("Email veya şifre hatalı"));

            return Ok(ApiResponse<string>.Ok(token, "Giriş başarılı"));
        }
    }
}
