using Microsoft.AspNetCore.Mvc;
using EnergsoftInterview.Api.Services;

namespace EnergsoftInterview.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtAuthenticationService _jwtService;

        public AuthController(JwtAuthenticationService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromHeader(Name = "X-API-Key")] string apiKey)
        {
            try
            {
                var token = await _jwtService.GenerateTokenAsync(apiKey);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid API key");
            }
        }
    }
} 