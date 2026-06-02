namespace HorsePedigree_2026.DTOs.Reportes;

public class EquinosPorEstadoItemResponse
{
    public long? EstadoId { get; set; }
    public string EstadoDescripcion { get; set; } = string.Empty;
    public int TotalEquinos { get; set; }
}
