using HorsePedigree_2026.Data;
using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Mappings;
using HorsePedigree_2026.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Services;

public class CampeonatoService : ServiceBase<Campeonato>, ICampeonatoService
{
    private readonly ICampeonatoRepository _campeonatoRepository;
    private readonly ApplicationDbContext _context;

    public CampeonatoService(ICampeonatoRepository repository, ApplicationDbContext context) : base(repository)
    {
        _campeonatoRepository = repository;
        _context = context;
    }

    public new async Task<IReadOnlyList<CampeonatoResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var campeonatos = await _campeonatoRepository.GetAllAsync(cancellationToken);
        return campeonatos.Select(CampeonatoMapper.ToResponse).ToList();
    }

    public async Task<CampeonatoResponse> CreateAsync(
        CreateCampeonatoRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidarCamposObligatorios(request);

        var campeonato = CampeonatoMapper.ToEntity(request);
        await _campeonatoRepository.AddAsync(campeonato, cancellationToken);

        return CampeonatoMapper.ToResponse(campeonato);
    }

    public async Task<CampeonatoResponse> GetDetalleAsync(long id, CancellationToken cancellationToken = default)
    {
        var campeonato = await _campeonatoRepository.GetByIdAsync(id, cancellationToken);
        if (campeonato is null)
        {
            throw new NotFoundException(nameof(Campeonato), id);
        }

        return CampeonatoMapper.ToResponse(campeonato);
    }

    public async Task<CampeonatoResponse> UpdateAsync(
        long id,
        UpdateCampeonatoRequest request,
        CancellationToken cancellationToken = default)
    {
        var campeonato = await _campeonatoRepository.GetByIdAsync(id, cancellationToken);
        if (campeonato is null)
        {
            throw new NotFoundException(nameof(Campeonato), id);
        }

        CampeonatoMapper.ApplyUpdate(campeonato, request);
        await _campeonatoRepository.UpdateAsync(campeonato, cancellationToken);

        return CampeonatoMapper.ToResponse(campeonato);
    }

    public new async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var campeonato = await _campeonatoRepository.GetByIdAsync(id, cancellationToken);
        if (campeonato is null)
        {
            throw new NotFoundException(nameof(Campeonato), id);
        }

        var tieneParticipaciones = await _context.EquinoCampeonatos
            .AnyAsync(ec => ec.CampeonatoId == id, cancellationToken);

        if (tieneParticipaciones)
        {
            throw new BusinessException(
                "No se puede eliminar el campeonato porque tiene participaciones de equinos registradas.");
        }

        await _campeonatoRepository.DeleteAsync(campeonato, cancellationToken);
    }

    private static void ValidarCamposObligatorios(CreateCampeonatoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            throw new BusinessException("El nombre es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Ubicacion))
        {
            throw new BusinessException("La ubicación es obligatoria.");
        }

        if (string.IsNullOrWhiteSpace(request.Nivel))
        {
            throw new BusinessException("El nivel es obligatorio.");
        }
    }
}
