using System.ComponentModel.DataAnnotations;

namespace HorsePedigree_2026.DTOs.Equinos;

public class InscribirEquinoCampeonatoRequest
{
    [Required(ErrorMessage = "El campeonato es obligatorio.")]
    public long CampeonatoId { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria.")]
    public long CategoriaId { get; set; }
}
