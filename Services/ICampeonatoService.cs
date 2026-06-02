using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Services;

public interface ICampeonatoService : IService<Campeonato>
{
    new Task<IReadOnlyList<CampeonatoResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CampeonatoResponse> CreateAsync(CreateCampeonatoRequest request, CancellationToken cancellationToken = default);
    Task<CampeonatoResponse> GetDetalleAsync(long id, CancellationToken cancellationToken = default);
    Task<CampeonatoResponse> UpdateAsync(long id, UpdateCampeonatoRequest request, CancellationToken cancellationToken = default);
    new Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}
