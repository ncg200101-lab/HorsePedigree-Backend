using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
{
    public CategoriaService(ICategoriaRepository repository) : base(repository)
    {
    }
}
