using HorsePedigree_2026.DTOs.Auth;
using HorsePedigree_2026.DTOs.Usuarios;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Services;

public interface IUsuarioService : IService<Usuario>
{
    Task<AuthenticatedUserResponse> RegisterAsync(
        CreateUsuarioRequest request,
        CancellationToken cancellationToken = default);
}
