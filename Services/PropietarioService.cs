using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class PropietarioService : ServiceBase<Propietario>, IPropietarioService
{
    public PropietarioService(IPropietarioRepository repository) : base(repository)
    {
    }
}
