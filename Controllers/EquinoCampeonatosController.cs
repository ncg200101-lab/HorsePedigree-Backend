using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorsePedigree_2026.Controllers;
[ApiController]
[Route("api/v1/equino-campeonatos")]
public class EquinoCampeonatosController : ControllerBase
{
    private readonly IEquinoCampeonatoService _equinoCampeonatoService;

    public EquinoCampeonatosController(IEquinoCampeonatoService equinoCampeonatoService)
    {
        _equinoCampeonatoService = equinoCampeonatoService;
    }

    /// <summary>
    /// Detalle de una participación en campeonato.
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(EquinoCampeonatoDetalleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoCampeonatoDetalleResponse>> GetByIdAsync(
        long id,
        CancellationToken cancellationToken)
    {
        var detalle = await _equinoCampeonatoService.GetDetalleAsync(id, cancellationToken);
        return Ok(detalle);
    }

    /// <summary>
    /// Actualiza parcialmente el resultado de una participación: victoria/derrota, puntaje,
    /// posición o categoría. Solo es necesario enviar los campos que se desean modificar.
    /// </summary>
    [HttpPut("{id:long}")]
    [Authorize]
    [ProducesResponseType(typeof(EquinoCampeonatoDetalleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquinoCampeonatoDetalleResponse>> UpdateAsync(
        long id,
        [FromBody] UpdateEquinoCampeonatoRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var detalle = await _equinoCampeonatoService.UpdateAsync(id, request, cancellationToken);
        return Ok(detalle);
    }

    /// <summary>
    /// Quita la participación de un equino en un campeonato.
    /// </summary>
    [HttpDelete("{id:long}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        await _equinoCampeonatoService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
