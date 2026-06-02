using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class EstadoRepository : Repository<Estado>, IEstadoRepository
{
    public EstadoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
