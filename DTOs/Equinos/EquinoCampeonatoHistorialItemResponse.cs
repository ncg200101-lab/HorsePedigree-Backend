namespace HorsePedigree_2026.DTOs.Equinos;

public class EquinoCampeonatoHistorialItemResponse
{
    public long EquinoCampeonatoId { get; set; }
    public long CampeonatoId { get; set; }
    public string CampeonatoNombre { get; set; } = string.Empty;
    public DateOnly FechaCampeonato { get; set; }
    public string CampeonatoUbicacion { get; set; } = string.Empty;
    public string CampeonatoNivel { get; set; } = string.Empty;
    public long CategoriaId { get; set; }
    public string CategoriaDescripcion { get; set; } = string.Empty;
    public string Resultado { get; set; } = string.Empty;
    public decimal Puntaje { get; set; }
    public long Posicion { get; set; }
}
