namespace HorsePedigree_2026.DTOs.Equinos;

public class EquinoReferenciaDto
{
    public long Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class CatalogoReferenciaDto
{
    public long Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}

public class PropietarioReferenciaDto
{
    public long Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Alias { get; set; }
}
