namespace HorsePedigree_2026.Entities;

public class Criadero
{
    public long CriaderoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Nit { get; set; }
    public string? Telefono { get; set; }

    public ICollection<Equino> Equinos { get; set; } = new List<Equino>();
}
