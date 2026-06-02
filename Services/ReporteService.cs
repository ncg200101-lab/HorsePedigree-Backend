using HorsePedigree_2026.Constants;
using HorsePedigree_2026.Data;
using HorsePedigree_2026.DTOs.Reportes;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Services.Export;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Services;

public class ReporteService : IReporteService
{
    private readonly ApplicationDbContext _context;
    private readonly IEquinosPorEstadoPdfGenerator _equinosPorEstadoPdfGenerator;

    public ReporteService(
        ApplicationDbContext context,
        IEquinosPorEstadoPdfGenerator equinosPorEstadoPdfGenerator)
    {
        _context = context;
        _equinosPorEstadoPdfGenerator = equinosPorEstadoPdfGenerator;
    }

    public async Task<IReadOnlyList<EquinosPorEstadoItemResponse>> GetEquinosPorEstadoAsync(
        CancellationToken cancellationToken = default)
    {
        return await (
            from equino in _context.Equinos.AsNoTracking()
            join estado in _context.Estados.AsNoTracking()
                on equino.EstadoId equals estado.EstadoId into estadoJoin
            from estado in estadoJoin.DefaultIfEmpty()
            group equino by new { equino.EstadoId, Descripcion = estado != null ? estado.Descripcion : null }
            into grupo
            orderby grupo.Key.Descripcion ?? "Sin estado"
            select new EquinosPorEstadoItemResponse
            {
                EstadoId = grupo.Key.EstadoId,
                EstadoDescripcion = grupo.Key.Descripcion ?? "Sin estado",
                TotalEquinos = grupo.Count()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<EstadisticasGeneralesResponse> GetEstadisticasGeneralesAsync(
        CancellationToken cancellationToken = default)
    {
        var totalEquinos = await _context.Equinos.AsNoTracking().CountAsync(cancellationToken);

        var equinosFallecidos = await _context.Equinos
            .AsNoTracking()
            .CountAsync(
                e => e.Estado != null && EF.Functions.ILike(e.Estado.Descripcion, "%fallec%"),
                cancellationToken);

        var equinosEnCompetencia = await _context.Equinos
            .AsNoTracking()
            .CountAsync(e => e.EnCompetencia, cancellationToken);

        var totalCampeonatos = await _context.Campeonatos.AsNoTracking().CountAsync(cancellationToken);

        var resultados = await _context.EquinoCampeonatos
            .AsNoTracking()
            .Select(ec => ec.Resultado)
            .ToListAsync(cancellationToken);

        var totalVictorias = resultados.Count(ResultadosCampeonato.EsVictoria);
        var totalDerrotas = resultados.Count(ResultadosCampeonato.EsDerrota);
        var totalParticipaciones = resultados.Count;
        var participacionesConResultado = totalVictorias + totalDerrotas;

        decimal? porcentajeVictorias = participacionesConResultado > 0
            ? Math.Round((decimal)totalVictorias / participacionesConResultado * 100, 2)
            : null;

        return new EstadisticasGeneralesResponse
        {
            TotalEquinos = totalEquinos,
            EquinosVivos = totalEquinos - equinosFallecidos,
            EquinosFallecidos = equinosFallecidos,
            EquinosEnCompetencia = equinosEnCompetencia,
            TotalCampeonatos = totalCampeonatos,
            TotalParticipaciones = totalParticipaciones,
            TotalVictorias = totalVictorias,
            TotalDerrotas = totalDerrotas,
            ParticipacionesSinResultado = totalParticipaciones - participacionesConResultado,
            PorcentajeVictorias = porcentajeVictorias
        };
    }

    public async Task<ReporteExportResult> ExportEquinosPorEstadoAsync(
        string formato,
        CancellationToken cancellationToken = default)
    {
        if (!ReporteFormatos.EsPdf(formato))
        {
            throw new BusinessException(
                $"El formato '{formato}' no es compatible. Use formato=pdf.");
        }

        var datos = await GetEquinosPorEstadoAsync(cancellationToken);
        var generadoEn = DateTime.UtcNow;
        var contenido = _equinosPorEstadoPdfGenerator.Generar(datos, generadoEn);

        return new ReporteExportResult
        {
            Content = contenido,
            ContentType = "application/pdf",
            FileName = $"equinos-por-estado-{generadoEn:yyyyMMdd-HHmmss}.pdf"
        };
    }
}