using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class CriaderoRepository : Repository<Criadero>, ICriaderoRepository
{
    public CriaderoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
