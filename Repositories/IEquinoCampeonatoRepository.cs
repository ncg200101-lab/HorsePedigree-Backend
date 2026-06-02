using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Repositories;

public interface IEquinoCampeonatoRepository : IRepository<EquinoCampeonato>
{
    Task<EquinoCampeonato?> GetByIdWithDetailsAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EquinoCampeonato>> GetByEquinoIdWithDetailsAsync(
        long equinoId,
        CancellationToken cancellationToken = default);
}
