using HorsePedigree_2026.Data;
using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Repositories;

public class EquinoRepository : Repository<Equino>, IEquinoRepository
{
    public EquinoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Equino?> GetByIdWithRelationsAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(e => e.Estado)
            .Include(e => e.Criadero)
            .Include(e => e.TipoDePaso)
            .Include(e => e.Propietario)
            .Include(e => e.Padre)
            .Include(e => e.Madre)
            .FirstOrDefaultAsync(e => e.EquinoId == id, cancellationToken);
    }

    public async Task<Equino?> GetByIdForGenealogiaAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(e => e.Padre).ThenInclude(p => p!.Padre)
            .Include(e => e.Padre).ThenInclude(p => p!.Madre)
            .Include(e => e.Madre).ThenInclude(m => m!.Padre)
            .Include(e => e.Madre).ThenInclude(m => m!.Madre)
            .FirstOrDefaultAsync(e => e.EquinoId == id, cancellationToken);
    }

    public async Task<Equino?> GetByIdForUpdateAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(e => e.Estado)
            .FirstOrDefaultAsync(e => e.EquinoId == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Equino>> BuscarAsync(EquinoFiltrosQuery filtros, CancellationToken cancellationToken = default)
    {
        var query = DbSet
            .AsNoTracking()
            .Include(e => e.Estado)
            .Include(e => e.Propietario)
            .Include(e => e.TipoDePaso)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtros.Nombre))
        {
            var nombre = filtros.Nombre.Trim();
            query = query.Where(e => EF.Functions.ILike(e.Nombre, $"%{nombre}%"));
        }

        if (filtros.EstadoId.HasValue)
        {
            query = query.Where(e => e.EstadoId == filtros.EstadoId.Value);
        }

        if (filtros.PropietarioId.HasValue)
        {
            query = query.Where(e => e.PropietarioId == filtros.PropietarioId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filtros.Propietario))
        {
            var propietario = filtros.Propietario.Trim();
            query = query.Where(e =>
                e.Propietario != null && (
                    EF.Functions.ILike(e.Propietario.Nombre, $"%{propietario}%") ||
                    EF.Functions.ILike(e.Propietario.Apellido, $"%{propietario}%") ||
                    (e.Propietario.Alias != null && EF.Functions.ILike(e.Propietario.Alias, $"%{propietario}%"))));
        }

        if (filtros.SoloVivos == true)
        {
            query = query.Where(e =>
                e.Estado == null ||
                !EF.Functions.ILike(e.Estado.Descripcion, "%fallec%"));
        }

        return await query
            .OrderBy(e => e.Nombre)
            .ToListAsync(cancellationToken);
    }
}
