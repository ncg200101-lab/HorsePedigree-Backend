namespace HorsePedigree_2026.DTOs.Equinos;

/// <summary>
/// Línea genealógica de un equino: padre y madre, con abuelos cuando están registrados.
/// </summary>
public class EquinoGenealogiaResponse
{
    public long EquinoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public EquinoGenealogiaAncestroResponse? Padre { get; set; }
    public EquinoGenealogiaAncestroResponse? Madre { get; set; }
}

/// <summary>
/// Ancestro directo (padre o madre del equino consultado) y sus padres (abuelos), si existen en BD.
/// </summary>
public class EquinoGenealogiaAncestroResponse
{
    public long EquinoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public EquinoReferenciaDto? Padre { get; set; }
    public EquinoReferenciaDto? Madre { get; set; }
}
