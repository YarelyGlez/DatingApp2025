using System.Security.Claims;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    //Implementacion del metodo de la interfaz
    public string CreateToken(AppUser user)
    {
        var tokenKey = configuration["TokenKey"] ?? throw new ArgumentNullException("Cannot get the token key");

        if (tokenKey.Length < 64)
            throw new ArgumentException("Token key must be >= 64 characters long");

        var key = new SymetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email)
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
