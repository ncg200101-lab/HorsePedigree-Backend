using System.Security.Claims;
using HorsePedigree_2026.Constants;
using HorsePedigree_2026.DTOs.Auth;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorsePedigree_2026.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Inicia sesión con username o email y contraseña. Devuelve un token JWT.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> LoginAsync(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var response = await _authService.LoginAsync(request, cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Devuelve la información del usuario autenticado según el token JWT enviado.
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthenticatedUserResponse>> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var usuarioIdClaim = User.FindFirstValue(AuthClaimTypes.UsuarioId)
            ?? User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!long.TryParse(usuarioIdClaim, out var usuarioId))
        {
            return Unauthorized();
        }

        var user = await _authService.GetCurrentUserAsync(usuarioId, cancellationToken);
        return Ok(user);
    }

    /// <summary>
    /// Cierra la sesión del usuario autenticado invalidando el token JWT actual.
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAsync(CancellationToken cancellationToken)
    {
        await _authService.LogoutAsync(User, cancellationToken);
        return NoContent();
    }
}