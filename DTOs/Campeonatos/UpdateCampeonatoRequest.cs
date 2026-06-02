using System.ComponentModel.DataAnnotations;
using HorsePedigree_2026.Helpers;

namespace HorsePedigree_2026.DTOs.Campeonatos;

public class UpdateCampeonatoRequest : IValidatableObject
{
    public Optional<string> Nombre { get; set; }
    public Optional<DateOnly> FechaCampeonato { get; set; }
    public Optional<string> Ubicacion { get; set; }
    public Optional<string?> Descripcion { get; set; }
    public Optional<string> Nivel { get; set; }

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

        if (Ubicacion.HasValue)
        {
            if (string.IsNullOrWhiteSpace(Ubicacion.Value))
            {
                yield return new ValidationResult(
                    "La ubicación no puede estar vacía.",
                    [nameof(Ubicacion)]);
            }
            else if (Ubicacion.Value!.Length > 500)
            {
                yield return new ValidationResult(
                    "La ubicación no puede superar los 500 caracteres.",
                    [nameof(Ubicacion)]);
            }
        }

        if (Descripcion.HasValue && Descripcion.Value?.Length > 2000)
        {
            yield return new ValidationResult(
                "La descripción no puede superar los 2000 caracteres.",
                [nameof(Descripcion)]);
        }

        if (Nivel.HasValue)
        {
            if (string.IsNullOrWhiteSpace(Nivel.Value))
            {
                yield return new ValidationResult(
                    "El nivel no puede estar vacío.",
                    [nameof(Nivel)]);
            }
            else if (Nivel.Value!.Length > 100)
            {
                yield return new ValidationResult(
                    "El nivel no puede superar los 100 caracteres.",
                    [nameof(Nivel)]);
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
        FechaCampeonato.HasValue ||
        Ubicacion.HasValue ||
        Descripcion.HasValue ||
        Nivel.HasValue;
}
