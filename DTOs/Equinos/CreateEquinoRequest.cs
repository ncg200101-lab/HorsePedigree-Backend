using System.ComponentModel.DataAnnotations;

namespace HorsePedigree_2026.DTOs.Equinos;

public class CreateEquinoRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(500)]
    public string Nombre { get; set; } = string.Empty;

    public string? TipoDeSangre { get; set; }
    public long? EstadoId { get; set; }
    public DateOnly? FechaDeNacimiento { get; set; }
    public long? CriaderoId { get; set; }
    public string? Descripcion { get; set; }
    public string? Sexo { get; set; }
    public string? ChipId { get; set; }
    public bool? Capon { get; set; }
    public bool? Mular { get; set; }
    public bool EnCompetencia { get; set; }
    public long? TipoDePasoId { get; set; }
    public long? PropietarioId { get; set; }
    public long? PadreId { get; set; }
    public long? MadreId { get; set; }
}
