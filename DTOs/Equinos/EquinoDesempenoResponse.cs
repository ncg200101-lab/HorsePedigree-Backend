namespace HorsePedigree_2026.DTOs.Equinos;

public class EquinoDesempenoResponse
{
    public long EquinoId { get; set; }
    public string EquinoNombre { get; set; } = string.Empty;
    public int TotalCampeonatos { get; set; }
    public int Victorias { get; set; }
    public int Derrotas { get; set; }
}
