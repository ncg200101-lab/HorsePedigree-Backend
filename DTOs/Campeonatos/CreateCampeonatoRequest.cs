using System.ComponentModel.DataAnnotations;

namespace HorsePedigree_2026.DTOs.Campeonatos;

public class CreateCampeonatoRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(500)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha del campeonato es obligatoria.")]
    public DateOnly FechaCampeonato { get; set; }

    [Required(ErrorMessage = "La ubicación es obligatoria.")]
    [MaxLength(500)]
    public string Ubicacion { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El nivel es obligatorio.")]
    [MaxLength(100)]
    public string Nivel { get; set; } = string.Empty;
}
