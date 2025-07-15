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

        /// <summary>
        /// Logs in a user and returns a JWT token for authentication.
        /// </summary>
        /// <param name="request">The username and password of the user.</param>
        /// <returns>
        /// 200 OK with JWT token if login is successful.  
        /// 401 Unauthorized if credentials are invalid.
        /// </returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The username and password for the new account.</param>
        /// <returns>
        /// 200 OK if registration is successful.  
        /// 400 Bad Request if data is invalid.  
        /// 409 Conflict if username already exists.
        /// </returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
        public IActionResult Register([FromBody] RegisterDto request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Username and password are required." });

            bool success = _authService.Register(request.Username, request.Password);

            if (!success)
                return Conflict(new { message = "Username already exists." });

            return Ok(new { message = "User registered successfully." });
        }
    }
}
