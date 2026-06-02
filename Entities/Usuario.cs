namespace HorsePedigree_2026.Entities;

public class Usuario
{
    public long UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long RolId { get; set; }
    public DateOnly FechaDeCreacion { get; set; }

    public Rol Rol { get; set; } = null!;
}
