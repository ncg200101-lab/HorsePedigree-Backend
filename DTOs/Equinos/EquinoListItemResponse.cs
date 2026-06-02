namespace HorsePedigree_2026.DTOs.Equinos;

public class EquinoListItemResponse
{
    public long EquinoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? TipoDeSangre { get; set; }
    public long? EstadoId { get; set; }
    public string? EstadoDescripcion { get; set; }
    public long? PropietarioId { get; set; }
    public string? PropietarioNombreCompleto { get; set; }
    public long? TipoDePasoId { get; set; }
    public string? TipoDePasoDescripcion { get; set; }
    public DateOnly? FechaDeFallecimiento { get; set; }
    public DateOnly FechaDeCreacion { get; set; }
}
