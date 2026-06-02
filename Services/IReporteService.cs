using HorsePedigree_2026.DTOs.Reportes;

namespace HorsePedigree_2026.Services;

public interface IReporteService
{
    Task<IReadOnlyList<EquinosPorEstadoItemResponse>> GetEquinosPorEstadoAsync(
        CancellationToken cancellationToken = default);

    Task<EstadisticasGeneralesResponse> GetEstadisticasGeneralesAsync(
        CancellationToken cancellationToken = default);

    Task<ReporteExportResult> ExportEquinosPorEstadoAsync(
        string formato,
        CancellationToken cancellationToken = default);
}