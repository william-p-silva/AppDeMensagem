

using AppDeMensagem.Application.Interfaces.Security;
using AppDeMensagem.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppDeMensagem.Infrastructure.Data.Security;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenereteToken(Usuario user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]
            ?? throw new InvalidOperationException("A chave secreta do JWT (Jwt:Key) não foi configurada no appsettings.json.")));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, user.User_ID.ToString()),
            new Claim(ClaimTypes.Email, user.EmailAddress.ToString()),
            new Claim(ClaimTypes.Name, user.UserName.ToString()),
            new Claim(ClaimTypes.Role, user.UserProfile.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Subject = new ClaimsIdentity(claims),
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddHours(2),
        };

        var handle = new JwtSecurityTokenHandler();
        var token = handle.CreateToken(tokenDescriptor);

        return handle.WriteToken(token);
    }
}
