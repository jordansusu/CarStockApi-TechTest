// Generates JWT tokens for authenticated dealers.

using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CarStockApi.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(int dealerId, string username)
    {
        // Check secretKey for generating token
        var secretKey = _config["Jwt:SecretKey"] 
            ?? throw new InvalidOperationException("JWT SecretKey is not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var handler = new JsonWebTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["jwt:Audience"],

            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, dealerId.ToString()),
                new Claim(ClaimTypes.Name, username)
            ]),

            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        return handler.CreateToken(tokenDescriptor);
    }
}