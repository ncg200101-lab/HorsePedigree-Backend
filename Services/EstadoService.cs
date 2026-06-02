using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class EstadoService : ServiceBase<Estado>, IEstadoService
{
    public EstadoService(IEstadoRepository repository) : base(repository)
    {
    }
}
