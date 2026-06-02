namespace HorsePedigree_2026.Entities;

public class TipoDePaso
{
    public long TipoPasoId { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Equino> Equinos { get; set; } = new List<Equino>();
}
