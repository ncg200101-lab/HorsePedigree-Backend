using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HorsePedigree_2026.Configuration;
using HorsePedigree_2026.Constants;
using HorsePedigree_2026.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HorsePedigree_2026.Services;

public interface IJwtTokenService
{
    (string Token, DateTime ExpiresAtUtc) CreateToken(Usuario usuario);
}

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public (string Token, DateTime ExpiresAtUtc) CreateToken(Usuario usuario)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, usuario.UsuarioId.ToString()),
            new(AuthClaimTypes.UsuarioId, usuario.UsuarioId.ToString()),
            new(AuthClaimTypes.Username, usuario.Username),
            new(AuthClaimTypes.Email, usuario.Email),
            new(AuthClaimTypes.RolId, usuario.RolId.ToString()),
            new(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}".Trim())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAtUtc);
    }
}
