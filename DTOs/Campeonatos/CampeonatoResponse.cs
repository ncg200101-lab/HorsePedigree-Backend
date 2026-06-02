namespace HorsePedigree_2026.DTOs.Campeonatos;

public class CampeonatoResponse
{
    public long CampeonatoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateOnly FechaCampeonato { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Nivel { get; set; } = string.Empty;
}
