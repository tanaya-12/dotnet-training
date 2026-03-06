using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/[controller]")]
// Don't include Authorize at this controller
// [Authorize]
public class AuthController : ControllerBase
{
    TokenService _tokenService;
    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        // In a real application, you would validate the user's credentials here
        // and generate a JWT token if the credentials are valid.

        if (loginRequest.Username == "sarah" && loginRequest.Password == "s@123")
        {
            var token = _tokenService.GenerateToken(loginRequest.Username, "Admin");
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}

public record LoginRequest(string Username, string Password);