using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HorsePedigree_2026.DTOs.Auth;
using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Helpers;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ITokenBlacklistService _tokenBlacklistService;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IJwtTokenService jwtTokenService,
        ITokenBlacklistService tokenBlacklistService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenService = jwtTokenService;
        _tokenBlacklistService = tokenBlacklistService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.GetByUsernameOrEmailAsync(request.Username, cancellationToken);
        if (usuario is null || !PasswordHelper.Verify(request.Password, usuario.Password))
        {
            throw new UnauthorizedException("Credenciales inválidas.");
        }

        var (token, expiresAtUtc) = _jwtTokenService.CreateToken(usuario);

        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAtUtc,
            User = MapToAuthenticatedUser(usuario)
        };
    }

    public async Task<AuthenticatedUserResponse> GetCurrentUserAsync(
        long usuarioId,
        CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.GetByIdWithRolAsync(usuarioId, cancellationToken);
        if (usuario is null)
        {
            throw new UnauthorizedException("La sesión ya no es válida.");
        }

        return MapToAuthenticatedUser(usuario);
    }

    public Task LogoutAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default)
    {
        var jti = user.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var expClaim = user.FindFirstValue(JwtRegisteredClaimNames.Exp);

        if (string.IsNullOrWhiteSpace(jti) || !long.TryParse(expClaim, out var expUnix))
        {
            throw new UnauthorizedException("No se pudo cerrar la sesión con el token actual.");
        }

        var expiresAtUtc = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
        _tokenBlacklistService.Revoke(jti, expiresAtUtc);

        return Task.CompletedTask;
    }

    private static AuthenticatedUserResponse MapToAuthenticatedUser(Usuario usuario)
    {
        return new AuthenticatedUserResponse
        {
            UsuarioId = usuario.UsuarioId,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Username = usuario.Username,
            Email = usuario.Email,
            RolId = usuario.RolId,
            RolDescripcion = usuario.Rol?.Descripcion ?? string.Empty
        };
    }
}
