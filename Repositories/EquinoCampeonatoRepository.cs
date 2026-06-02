using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Repositories;

public class EquinoCampeonatoRepository : Repository<EquinoCampeonato>, IEquinoCampeonatoRepository
{
    public EquinoCampeonatoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<EquinoCampeonato?> GetByIdWithDetailsAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Include(ec => ec.Equino)
            .Include(ec => ec.Campeonato)
            .Include(ec => ec.Categoria)
            .FirstOrDefaultAsync(ec => ec.EquinoCampeonatoId == id, cancellationToken);
    }

    public async Task<IReadOnlyList<EquinoCampeonato>> GetByEquinoIdWithDetailsAsync(
        long equinoId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Include(ec => ec.Campeonato)
            .Include(ec => ec.Categoria)
            .Where(ec => ec.EquinoId == equinoId)
            .OrderByDescending(ec => ec.Campeonato.FechaCampeonato)
            .ThenBy(ec => ec.Campeonato.Nombre)
            .ToListAsync(cancellationToken);
    }
}
