using HorsePedigree_2026.DTOs.Equinos;

namespace HorsePedigree_2026.Services;

public interface IEquinoService
{
    Task<EquinoResponse> CreateAsync(CreateEquinoRequest request, CancellationToken cancellationToken = default);
    Task<EquinoResponse> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<EquinoGenealogiaResponse> GetGenealogiaAsync(long id, CancellationToken cancellationToken = default);
    Task<EquinoResponse> UpdateAsync(long id, UpdateEquinoRequest request, CancellationToken cancellationToken = default);
    Task<EquinoResponse> CambiarEstadoAsync(long id, CambiarEstadoEquinoRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EquinoListItemResponse>> GetAllAsync(EquinoFiltrosQuery filtros, CancellationToken cancellationToken = default);
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
