using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class RolRepository : Repository<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext context) : base(context)
    {
    }
}
