using HorsePedigree_2026.Constants;
using HorsePedigree_2026.Data;
using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Mappings;
using HorsePedigree_2026.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Services;

public class EquinoCampeonatoService : ServiceBase<EquinoCampeonato>, IEquinoCampeonatoService
{
    private readonly IEquinoCampeonatoRepository _equinoCampeonatoRepository;
    private readonly ApplicationDbContext _context;

    public EquinoCampeonatoService(
        IEquinoCampeonatoRepository repository,
        ApplicationDbContext context) : base(repository)
    {
        _equinoCampeonatoRepository = repository;
        _context = context;
    }

    public async Task<EquinoCampeonatoDetalleResponse> GetDetalleAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var inscripcion = await _equinoCampeonatoRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        if (inscripcion is null)
        {
            throw new NotFoundException(nameof(EquinoCampeonato), id);
        }

        return EquinoCampeonatoMapper.ToDetalle(inscripcion);
    }

    public async Task<EquinoCampeonatoDetalleResponse> UpdateAsync(
        long id,
        UpdateEquinoCampeonatoRequest request,
        CancellationToken cancellationToken = default)
    {
        var inscripcion = await _equinoCampeonatoRepository.GetByIdAsync(id, cancellationToken);
        if (inscripcion is null)
        {
            throw new NotFoundException(nameof(EquinoCampeonato), id);
        }

        if (request.CategoriaId.HasValue)
        {
            if (!await _context.Categorias.AnyAsync(
                    c => c.CategoriaId == request.CategoriaId.Value,
                    cancellationToken))
            {
                throw new BusinessException($"No existe una categoría con id {request.CategoriaId.Value}.");
            }

            var duplicado = await _context.EquinoCampeonatos
                .AnyAsync(
                    ec => ec.EquinoCampeonatoId != id
                        && ec.EquinoId == inscripcion.EquinoId
                        && ec.CampeonatoId == inscripcion.CampeonatoId
                        && ec.CategoriaId == request.CategoriaId.Value,
                    cancellationToken);

            if (duplicado)
            {
                throw new BusinessException(
                    "El equino ya está inscrito en ese campeonato y categoría.");
            }
        }

        EquinoCampeonatoMapper.ApplyUpdate(inscripcion, request);
        await _equinoCampeonatoRepository.UpdateAsync(inscripcion, cancellationToken);

        var actualizado = await _equinoCampeonatoRepository.GetByIdWithDetailsAsync(id, cancellationToken);
        return EquinoCampeonatoMapper.ToDetalle(actualizado!);
    }

    public async Task<IReadOnlyList<EquinoCampeonatoHistorialItemResponse>> GetHistorialAsync(
        long equinoId,
        CancellationToken cancellationToken = default)
    {
        var equinoExiste = await _context.Equinos
            .AnyAsync(e => e.EquinoId == equinoId, cancellationToken);

        if (!equinoExiste)
        {
            throw new NotFoundException(nameof(Equino), equinoId);
        }

        var inscripciones = await _equinoCampeonatoRepository
            .GetByEquinoIdWithDetailsAsync(equinoId, cancellationToken);

        return inscripciones.Select(EquinoCampeonatoMapper.ToHistorialItem).ToList();
    }

    public async Task<EquinoDesempenoResponse> GetDesempenoAsync(
        long equinoId,
        CancellationToken cancellationToken = default)
    {
        var equino = await _context.Equinos
            .AsNoTracking()
            .Where(e => e.EquinoId == equinoId)
            .Select(e => new { e.EquinoId, e.Nombre })
            .FirstOrDefaultAsync(cancellationToken);

        if (equino is null)
        {
            throw new NotFoundException(nameof(Equino), equinoId);
        }

        var resultados = await _context.EquinoCampeonatos
            .AsNoTracking()
            .Where(ec => ec.EquinoId == equinoId)
            .Select(ec => ec.Resultado)
            .ToListAsync(cancellationToken);

        return new EquinoDesempenoResponse
        {
            EquinoId = equino.EquinoId,
            EquinoNombre = equino.Nombre,
            TotalCampeonatos = resultados.Count,
            Victorias = resultados.Count(ResultadosCampeonato.EsVictoria),
            Derrotas = resultados.Count(ResultadosCampeonato.EsDerrota)
        };
    }

    public async Task<EquinoCampeonatoResponse> InscribirAsync(
        long equinoId,
        InscribirEquinoCampeonatoRequest request,
        CancellationToken cancellationToken = default)
    {
        var equinoExiste = await _context.Equinos
            .AnyAsync(e => e.EquinoId == equinoId, cancellationToken);

        if (!equinoExiste)
        {
            throw new NotFoundException(nameof(Equino), equinoId);
        }

        if (!await _context.Campeonatos.AnyAsync(c => c.CampeonatoId == request.CampeonatoId, cancellationToken))
        {
            throw new BusinessException($"No existe un campeonato con id {request.CampeonatoId}.");
        }

        if (!await _context.Categorias.AnyAsync(c => c.CategoriaId == request.CategoriaId, cancellationToken))
        {
            throw new BusinessException($"No existe una categoría con id {request.CategoriaId}.");
        }

        var yaInscrito = await _context.EquinoCampeonatos
            .AnyAsync(
                ec => ec.EquinoId == equinoId
                    && ec.CampeonatoId == request.CampeonatoId
                    && ec.CategoriaId == request.CategoriaId,
                cancellationToken);

        if (yaInscrito)
        {
            throw new BusinessException(
                "El equino ya está inscrito en ese campeonato y categoría.");
        }

        var inscripcion = EquinoCampeonatoMapper.ToEntity(equinoId, request);
        await _equinoCampeonatoRepository.AddAsync(inscripcion, cancellationToken);

        return EquinoCampeonatoMapper.ToResponse(inscripcion);
    }
}
