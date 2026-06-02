namespace HorsePedigree_2026.DTOs.Equinos;

/// <summary>
/// Filtros de consulta para el listado de equinos.
/// </summary>
public class EquinoFiltrosQuery
{
    /// <summary>Búsqueda parcial por nombre del equino (insensible a mayúsculas).</summary>
    public string? Nombre { get; set; }

    /// <summary>Filtrar por id de propietario (dueño).</summary>
    public long? PropietarioId { get; set; }

    /// <summary>Búsqueda parcial por nombre, apellido o alias del propietario.</summary>
    public string? Propietario { get; set; }

    /// <summary>Filtrar por id de estado.</summary>
    public long? EstadoId { get; set; }

    /// <summary>Si es true, excluye equinos cuyo estado contenga "fallec" en la descripción.</summary>
    public bool? SoloVivos { get; set; }
}
