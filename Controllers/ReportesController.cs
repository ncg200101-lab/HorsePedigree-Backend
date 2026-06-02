using HorsePedigree_2026.DTOs.Reportes;
using HorsePedigree_2026.Services;
using Microsoft.AspNetCore.Mvc;
using HorsePedigree_2026.Constants;
namespace HorsePedigree_2026.Controllers;

[ApiController]
[Route("api/v1/reportes")]
public class ReportesController : ControllerBase
{
    private readonly IReporteService _reporteService;

    public ReportesController(IReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    /// <summary>
    /// Caballos agrupados por estado.
    /// </summary>
    [HttpGet("equinos-por-estado")]
    [ProducesResponseType(typeof(IReadOnlyList<EquinosPorEstadoItemResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<EquinosPorEstadoItemResponse>>> GetEquinosPorEstadoAsync(
        CancellationToken cancellationToken)
    {
        var reporte = await _reporteService.GetEquinosPorEstadoAsync(cancellationToken);
        return Ok(reporte);
    }

    /// <summary>
    /// Exporta el reporte de equinos agrupados por estado.
    /// </summary>
    [HttpGet("equinos-por-estado/exportar")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExportarEquinosPorEstadoAsync(
        [FromQuery] string formato = ReporteFormatos.Pdf,
        CancellationToken cancellationToken = default)
    {
        var exportacion = await _reporteService.ExportEquinosPorEstadoAsync(formato, cancellationToken);
        return File(exportacion.Content, exportacion.ContentType, exportacion.FileName);
    }

    /// <summary>
    /// Totales generales: equinos vivos/fallecidos, campeonatos, participaciones y porcentaje de victorias.
    /// </summary>
    [HttpGet("estadisticas-generales")]
    [ProducesResponseType(typeof(EstadisticasGeneralesResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<EstadisticasGeneralesResponse>> GetEstadisticasGeneralesAsync(
        CancellationToken cancellationToken)
    {
        var estadisticas = await _reporteService.GetEstadisticasGeneralesAsync(cancellationToken);
        return Ok(estadisticas);
    }
}
