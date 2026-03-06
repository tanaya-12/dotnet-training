using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService
{
    // private readonly IConfiguration _configuration;

    // public TokenService(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }

    public string GenerateToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim("custom_claim", "custom_value")
        };

        // Generate a key - Save in appconfig.json
        // var key = RandomNumberGenerator.GetBytes(32); // 256 bits
        // var base64Key = Convert.ToBase64String(key);


        // hard coded 128 bit key for demo purposes, in a real application, store this securely and don't hard code it
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uWmCk8kF2G5Y3r0yP8dBv5rXjA1q9SxH6eZtL4QnM7U="));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "revature",
            audience: "dotnet-batch-2026",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}