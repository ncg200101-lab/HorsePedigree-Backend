using System.ComponentModel.DataAnnotations;

namespace HorsePedigree_2026.DTOs.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "El usuario es obligatorio.")]
    [MaxLength(255)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;
}
