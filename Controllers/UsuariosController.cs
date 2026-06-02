using HorsePedigree_2026.Constants;
using HorsePedigree_2026.DTOs.Auth;
using HorsePedigree_2026.DTOs.Usuarios;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorsePedigree_2026.Controllers;

[ApiController]
[Route("api/v1/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Registra un nuevo usuario. Solo administradores (rol_id = 1).
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AuthenticatedUserResponse>> RegisterAsync(
        [FromBody] CreateUsuarioRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var usuario = await _usuarioService.RegisterAsync(request, cancellationToken);
        return Created($"/api/v1/usuarios/{usuario.UsuarioId}", usuario);
    }
}
