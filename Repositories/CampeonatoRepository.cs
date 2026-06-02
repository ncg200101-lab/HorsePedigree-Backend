using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Repositories;

public class CampeonatoRepository : Repository<Campeonato>, ICampeonatoRepository
{
    public CampeonatoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IReadOnlyList<Campeonato>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .OrderByDescending(c => c.FechaCampeonato)
            .ThenBy(c => c.Nombre)
            .ToListAsync(cancellationToken);
    }
}
