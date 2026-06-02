using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class CriaderoService : ServiceBase<Criadero>, ICriaderoService
{
    public CriaderoService(ICriaderoRepository repository) : base(repository)
    {
    }
}
