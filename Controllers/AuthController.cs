using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SeinfeldAPI.Interfaces;
using SeinfeldAPI.Models.DTOs;

namespace SeinfeldAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request) 
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Username and password are required." });

            string token = _authService.Login(request.Username, request.Password);

            if (token == null)
                return Unauthorized(new { message = "Invalid username or password " });

            return Ok(new
            {
                message = "Login Successfull",
                token
            });
        }
    }
}
