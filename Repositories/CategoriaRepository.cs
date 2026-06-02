using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext context) : base(context)
    {
    }
}
