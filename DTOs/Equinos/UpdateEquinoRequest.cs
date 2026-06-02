using System.ComponentModel.DataAnnotations;
using HorsePedigree_2026.Helpers;

namespace HorsePedigree_2026.DTOs.Equinos;

public class UpdateEquinoRequest : IValidatableObject
{
    public Optional<string> Nombre { get; set; }

    public Optional<string?> TipoDeSangre { get; set; }
    public Optional<long?> EstadoId { get; set; }
    public Optional<DateOnly?> FechaDeNacimiento { get; set; }
    public Optional<DateOnly?> FechaDeFallecimiento { get; set; }
    public Optional<long?> CriaderoId { get; set; }
    public Optional<string?> Descripcion { get; set; }
    public Optional<string?> Sexo { get; set; }
    public Optional<string?> ChipId { get; set; }
    public Optional<bool?> Capon { get; set; }
    public Optional<bool?> Mular { get; set; }
    public Optional<bool> EnCompetencia { get; set; }
    public Optional<long?> TipoDePasoId { get; set; }
    public Optional<long?> PropietarioId { get; set; }
    public Optional<long?> PadreId { get; set; }
    public Optional<long?> MadreId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Nombre.HasValue)
        {
            if (string.IsNullOrWhiteSpace(Nombre.Value))
            {
                yield return new ValidationResult(
                    "El nombre no puede estar vacío.",
                    [nameof(Nombre)]);
            }
            else if (Nombre.Value!.Length > 500)
            {
                yield return new ValidationResult(
                    "El nombre no puede superar los 500 caracteres.",
                    [nameof(Nombre)]);
            }
        }

        if (!TieneAlMenosUnCampo())
        {
            yield return new ValidationResult(
                "Debe enviar al menos un campo para actualizar.",
                []);
        }
    }

    public bool TieneAlMenosUnCampo() =>
        Nombre.HasValue ||
        TipoDeSangre.HasValue ||
        EstadoId.HasValue ||
        FechaDeNacimiento.HasValue ||
        FechaDeFallecimiento.HasValue ||
        CriaderoId.HasValue ||
        Descripcion.HasValue ||
        Sexo.HasValue ||
        ChipId.HasValue ||
        Capon.HasValue ||
        Mular.HasValue ||
        EnCompetencia.HasValue ||
        TipoDePasoId.HasValue ||
        PropietarioId.HasValue ||
        PadreId.HasValue ||
        MadreId.HasValue;
}
