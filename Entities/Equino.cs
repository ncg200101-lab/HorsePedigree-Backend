namespace HorsePedigree_2026.Entities;

public class Equino
{
    public long EquinoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? TipoDeSangre { get; set; }
    public long? EstadoId { get; set; }
    public DateOnly? FechaDeNacimiento { get; set; }
    public DateOnly? FechaDeFallecimiento { get; set; }
    public long? CriaderoId { get; set; }
    public string? Descripcion { get; set; }
    public string? Sexo { get; set; }
    public string? ChipId { get; set; }
    public bool? Capon { get; set; }
    public bool? Mular { get; set; }
    public bool EnCompetencia { get; set; }
    public DateOnly FechaDeCreacion { get; set; }
    public DateOnly FechaDeActualizacion { get; set; }
    public long? TipoDePasoId { get; set; }
    public long? PropietarioId { get; set; }
    public long? PadreId { get; set; }
    public long? MadreId { get; set; }

    public Estado? Estado { get; set; }
    public Criadero? Criadero { get; set; }
    public TipoDePaso? TipoDePaso { get; set; }
    public Propietario? Propietario { get; set; }
    public Equino? Padre { get; set; }
    public Equino? Madre { get; set; }

    public ICollection<Equino> HijosComoPadre { get; set; } = new List<Equino>();
    public ICollection<Equino> HijosComoMadre { get; set; } = new List<Equino>();
    public ICollection<EquinoCampeonato> EquinoCampeonatos { get; set; } = new List<EquinoCampeonato>();
}
