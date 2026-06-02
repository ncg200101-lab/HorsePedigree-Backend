using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class RolService : ServiceBase<Rol>, IRolService
{
    public RolService(IRolRepository repository) : base(repository)
    {
    }
}
