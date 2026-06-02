using System.ComponentModel.DataAnnotations;
using HorsePedigree_2026.Constants;
using HorsePedigree_2026.Helpers;

namespace HorsePedigree_2026.DTOs.Equinos;

public class UpdateEquinoCampeonatoRequest : IValidatableObject
{
    public Optional<string> Resultado { get; set; }
    public Optional<decimal> Puntaje { get; set; }
    public Optional<long> Posicion { get; set; }
    public Optional<long> CategoriaId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Resultado.HasValue)
        {
            if (string.IsNullOrWhiteSpace(Resultado.Value))
            {
                yield return new ValidationResult(
                    "El resultado no puede estar vacío.",
                    [nameof(Resultado)]);
            }
            else if (!ResultadosCampeonato.EsValido(Resultado.Value))
            {
                yield return new ValidationResult(
                    "El resultado debe ser 'Victoria' o 'Derrota'.",
                    [nameof(Resultado)]);
            }
        }

        if (Puntaje.HasValue && Puntaje.Value < 0)
        {
            yield return new ValidationResult(
                "El puntaje no puede ser negativo.",
                [nameof(Puntaje)]);
        }

        if (Posicion.HasValue && Posicion.Value < 0)
        {
            yield return new ValidationResult(
                "La posición no puede ser negativa.",
                [nameof(Posicion)]);
        }

        if (CategoriaId.HasValue && CategoriaId.Value <= 0)
        {
            yield return new ValidationResult(
                "La categoría debe ser un id válido.",
                [nameof(CategoriaId)]);
        }

        if (!TieneAlMenosUnCampo())
        {
            yield return new ValidationResult(
                "Debe enviar al menos un campo para actualizar.",
                []);
        }
    }

    public bool TieneAlMenosUnCampo() =>
        Resultado.HasValue ||
        Puntaje.HasValue ||
        Posicion.HasValue ||
        CategoriaId.HasValue;
}
