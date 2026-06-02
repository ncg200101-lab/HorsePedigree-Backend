using HorsePedigree_2026.Constants;
using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorsePedigree_2026.Controllers;

[ApiController]
[Route("api/v1/campeonatos")]
public class CampeonatosController : ControllerBase
{
    private readonly ICampeonatoService _campeonatoService;

    public CampeonatosController(ICampeonatoService campeonatoService)
    {
        _campeonatoService = campeonatoService;
    }

    private const string CampeonatoByIdRoute = "GetCampeonatoById";

    /// <summary>
    /// Listado de campeonatos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<CampeonatoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CampeonatoResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var campeonatos = await _campeonatoService.GetAllAsync(cancellationToken);
        return Ok(campeonatos);
    }

    /// <summary>
    /// Registra un nuevo campeonato.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(CampeonatoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CampeonatoResponse>> CreateAsync(
        [FromBody] CreateCampeonatoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var campeonato = await _campeonatoService.CreateAsync(request, cancellationToken);
        return CreatedAtRoute(CampeonatoByIdRoute, new { id = campeonato.CampeonatoId }, campeonato);
    }

    /// <summary>
    /// Consulta un campeonato por id.
    /// </summary>
    [HttpGet("{id:long}", Name = CampeonatoByIdRoute)]
    [ProducesResponseType(typeof(CampeonatoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampeonatoResponse>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var campeonato = await _campeonatoService.GetDetalleAsync(id, cancellationToken);
        return Ok(campeonato);
    }

    /// <summary>
    /// Actualiza parcialmente un campeonato. Solo es necesario enviar
    /// los campos que se desean modificar; los omitidos conservan su valor actual.
    /// </summary>
    [HttpPut("{id:long}")]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(typeof(CampeonatoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CampeonatoResponse>> UpdateAsync(
        long id,
        [FromBody] UpdateCampeonatoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var campeonato = await _campeonatoService.UpdateAsync(id, request, cancellationToken);
        return Ok(campeonato);
    }

    /// <summary>
    /// Elimina un campeonato. Solo se permite si no tiene participaciones de equinos registradas.
    /// </summary>
    [HttpDelete("{id:long}")]
    [Authorize(Policy = AuthPolicies.Administrador)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _campeonatoService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
