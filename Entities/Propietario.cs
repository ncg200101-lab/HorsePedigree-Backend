namespace HorsePedigree_2026.Entities;

public class Propietario
{
    public long PropietarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Alias { get; set; }
    public string Apellido { get; set; } = string.Empty;
    public string? Cedula { get; set; }
    public string? Nit { get; set; }
    public DateTime FechaDeNacimiento { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public DateTime FechaDeIngreso { get; set; }

    public ICollection<Equino> Equinos { get; set; } = new List<Equino>();
}
