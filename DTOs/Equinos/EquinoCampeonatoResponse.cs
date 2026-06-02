namespace HorsePedigree_2026.DTOs.Equinos;

public class EquinoCampeonatoResponse
{
    public long EquinoCampeonatoId { get; set; }
    public long EquinoId { get; set; }
    public long CampeonatoId { get; set; }
    public long CategoriaId { get; set; }
    public string Resultado { get; set; } = string.Empty;
    public decimal Puntaje { get; set; }
    public long Posicion { get; set; }
}
