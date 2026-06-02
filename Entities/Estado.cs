namespace HorsePedigree_2026.Entities;

public class Estado
{
    public long EstadoId { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Equino> Equinos { get; set; } = new List<Equino>();
}
