using HorsePedigree_2026.DTOs.Auth;
using HorsePedigree_2026.DTOs.Usuarios;
using HorsePedigree_2026.Entities;
using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Helpers;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IRolRepository _rolRepository;

    public UsuarioService(IUsuarioRepository repository, IRolRepository rolRepository) : base(repository)
    {
        _usuarioRepository = repository;
        _rolRepository = rolRepository;
    }

    public async Task<AuthenticatedUserResponse> RegisterAsync(
        CreateUsuarioRequest request,
        CancellationToken cancellationToken = default)
    {
        var username = request.Username.Trim();
        var email = request.Email.Trim().ToLowerInvariant();

        if (await _usuarioRepository.ExistsByUsernameAsync(username, cancellationToken))
        {
            throw new BusinessException($"Ya existe un usuario con username '{username}'.");
        }

        if (await _usuarioRepository.ExistsByEmailAsync(email, cancellationToken))
        {
            throw new BusinessException($"Ya existe un usuario con email '{email}'.");
        }

        if (!await _rolRepository.ExistsAsync(request.RolId, cancellationToken))
        {
            throw new BusinessException($"No existe un rol con id {request.RolId}.");
        }

        var usuario = new Usuario
        {
            Nombre = request.Nombre.Trim(),
            Apellido = request.Apellido.Trim(),
            Username = username,
            Email = email,
            Password = PasswordHelper.Hash(request.Password),
            RolId = request.RolId
        };

        await _usuarioRepository.AddAsync(usuario, cancellationToken);

        var creado = await _usuarioRepository.GetByIdWithRolAsync(usuario.UsuarioId, cancellationToken);
        return MapToAuthenticatedUser(creado!);
    }

    private static AuthenticatedUserResponse MapToAuthenticatedUser(Usuario usuario)
    {
        return new AuthenticatedUserResponse
        {
            UsuarioId = usuario.UsuarioId,
            Nombre = usuario.Nombre,
            Apellido = usuario.Apellido,
            Username = usuario.Username,
            Email = usuario.Email,
            RolId = usuario.RolId,
            RolDescripcion = usuario.Rol?.Descripcion ?? string.Empty
        };
    }
}
