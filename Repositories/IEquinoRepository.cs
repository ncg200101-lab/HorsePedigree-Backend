using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public interface IEquinoRepository : IRepository<Equino>
{
    Task<Equino?> GetByIdWithRelationsAsync(long id, CancellationToken cancellationToken = default);
    Task<Equino?> GetByIdForGenealogiaAsync(long id, CancellationToken cancellationToken = default);
    Task<Equino?> GetByIdForUpdateAsync(long id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Equino>> BuscarAsync(EquinoFiltrosQuery filtros, CancellationToken cancellationToken = default);
}
