using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Models;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly ILogger<UserController> _logger;

        public UserController(JwtService jwtService, ILogger<UserController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // TODO: Validate user credentials against your user store
            if (model.Username == "admin" && model.Password == "password")
            {
                var token = _jwtService.GenerateToken("1", model.Username);
                _logger.LogInformation("User {Username} logged in successfully", model.Username);
                return Ok(new { Token = token });
            }

            _logger.LogWarning("Failed login attempt for user {Username}", model.Username);
            return Unauthorized();
        }
    }
}