using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class PropietarioRepository : Repository<Propietario>, IPropietarioRepository
{
    public PropietarioRepository(ApplicationDbContext context) : base(context)
    {
    }
}
