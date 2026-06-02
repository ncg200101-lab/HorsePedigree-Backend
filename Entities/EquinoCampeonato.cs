namespace HorsePedigree_2026.Entities;

public class EquinoCampeonato
{
    public long EquinoCampeonatoId { get; set; }
    public long EquinoId { get; set; }
    public long CampeonatoId { get; set; }
    public long CategoriaId { get; set; }
    public string Resultado { get; set; } = string.Empty;
    public decimal Puntaje { get; set; }
    public long Posicion { get; set; }

    public Equino Equino { get; set; } = null!;
    public Campeonato Campeonato { get; set; } = null!;
    public Categoria Categoria { get; set; } = null!;
}
