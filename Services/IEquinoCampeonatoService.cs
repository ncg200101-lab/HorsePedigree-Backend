using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Services;

public interface IEquinoCampeonatoService : IService<EquinoCampeonato>
{
    Task<EquinoCampeonatoDetalleResponse> GetDetalleAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<EquinoCampeonatoDetalleResponse> UpdateAsync(
        long id,
        UpdateEquinoCampeonatoRequest request,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EquinoCampeonatoHistorialItemResponse>> GetHistorialAsync(
        long equinoId,
        CancellationToken cancellationToken = default);

    Task<EquinoDesempenoResponse> GetDesempenoAsync(
        long equinoId,
        CancellationToken cancellationToken = default);

    Task<EquinoCampeonatoResponse> InscribirAsync(
        long equinoId,
        InscribirEquinoCampeonatoRequest request,
        CancellationToken cancellationToken = default);
}
