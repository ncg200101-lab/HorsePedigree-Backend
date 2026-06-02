namespace HorsePedigree_2026.DTOs.Reportes;

public class EstadisticasGeneralesResponse
{
    public int TotalEquinos { get; set; }
    public int EquinosVivos { get; set; }
    public int EquinosFallecidos { get; set; }
    public int EquinosEnCompetencia { get; set; }
    public int TotalCampeonatos { get; set; }
    public int TotalParticipaciones { get; set; }
    public int TotalVictorias { get; set; }
    public int TotalDerrotas { get; set; }
    public int ParticipacionesSinResultado { get; set; }
    public decimal? PorcentajeVictorias { get; set; }
}
