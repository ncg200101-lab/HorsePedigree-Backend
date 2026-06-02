using System.ComponentModel.DataAnnotations;

namespace HorsePedigree_2026.DTOs.Equinos;

public class CambiarEstadoEquinoRequest
{
    [Required(ErrorMessage = "El estado es obligatorio.")]
    public long EstadoId { get; set; }

    /// <summary>
    /// Opcional. Si el estado es fallecido y no se envía, se usa la fecha actual (UTC).
    /// </summary>
    public DateOnly? FechaDeFallecimiento { get; set; }
}
