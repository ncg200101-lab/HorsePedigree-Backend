namespace HorsePedigree_2026.DTOs.Auth;

public class AuthenticatedUserResponse
{
    public long UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long RolId { get; set; }
    public string RolDescripcion { get; set; } = string.Empty;
}
