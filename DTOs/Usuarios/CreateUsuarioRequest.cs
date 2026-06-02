using System.ComponentModel.DataAnnotations;

namespace HorsePedigree_2026.DTOs.Usuarios;

public class CreateUsuarioRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [MaxLength(255)]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El username es obligatorio.")]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    public long RolId { get; set; }
}
