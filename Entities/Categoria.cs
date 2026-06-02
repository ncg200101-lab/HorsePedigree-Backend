namespace HorsePedigree_2026.Entities;

public class Categoria
{
    public long CategoriaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<EquinoCampeonato> EquinoCampeonatos { get; set; } = new List<EquinoCampeonato>();
}
