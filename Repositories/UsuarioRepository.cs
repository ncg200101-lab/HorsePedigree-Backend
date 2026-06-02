using HorsePedigree_2026.Data;
using HorsePedigree_2026.Entities;
using Microsoft.EntityFrameworkCore;

namespace HorsePedigree_2026.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByUsernameOrEmailAsync(
        string usernameOrEmail,
        CancellationToken cancellationToken = default)
    {
        var normalized = usernameOrEmail.Trim().ToLowerInvariant();

        return await DbSet
            .AsNoTracking()
            .Include(x => x.Rol)
            .FirstOrDefaultAsync(
                x => x.Username.ToLower() == normalized || x.Email.ToLower() == normalized,
                cancellationToken);
    }

    public async Task<Usuario?> GetByIdWithRolAsync(long id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(x => x.Rol)
            .FirstOrDefaultAsync(x => x.UsuarioId == id, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var normalized = username.Trim().ToLowerInvariant();

        return await DbSet
            .AsNoTracking()
            .AnyAsync(x => x.Username.ToLower() == normalized, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalized = email.Trim().ToLowerInvariant();

        return await DbSet
            .AsNoTracking()
            .AnyAsync(x => x.Email.ToLower() == normalized, cancellationToken);
    }
}
