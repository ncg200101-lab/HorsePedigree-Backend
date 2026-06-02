using HorsePedigree_2026.Constants;
using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Entities;
namespace HorsePedigree_2026.Mappings;

public static class EquinoCampeonatoMapper
{
    public static EquinoCampeonato ToEntity(long equinoId, InscribirEquinoCampeonatoRequest request)
    {
        return new EquinoCampeonato
        {
            EquinoId = equinoId,
            CampeonatoId = request.CampeonatoId,
            CategoriaId = request.CategoriaId,
            Resultado = string.Empty,
            Puntaje = 0,
            Posicion = 0
        };
    }

    public static EquinoCampeonatoResponse ToResponse(EquinoCampeonato inscripcion)
    {
        return new EquinoCampeonatoResponse
        {
            EquinoCampeonatoId = inscripcion.EquinoCampeonatoId,
            EquinoId = inscripcion.EquinoId,
            CampeonatoId = inscripcion.CampeonatoId,
            CategoriaId = inscripcion.CategoriaId,
            Resultado = inscripcion.Resultado,
            Puntaje = inscripcion.Puntaje,
            Posicion = inscripcion.Posicion
        };
    }

    public static EquinoCampeonatoHistorialItemResponse ToHistorialItem(EquinoCampeonato inscripcion)
    {
        return new EquinoCampeonatoHistorialItemResponse
        {
            EquinoCampeonatoId = inscripcion.EquinoCampeonatoId,
            CampeonatoId = inscripcion.CampeonatoId,
            CampeonatoNombre = inscripcion.Campeonato.Nombre,
            FechaCampeonato = inscripcion.Campeonato.FechaCampeonato,
            CampeonatoUbicacion = inscripcion.Campeonato.Ubicacion,
            CampeonatoNivel = inscripcion.Campeonato.Nivel,
            CategoriaId = inscripcion.CategoriaId,
            CategoriaDescripcion = inscripcion.Categoria.Descripcion,
            Resultado = inscripcion.Resultado,
            Puntaje = inscripcion.Puntaje,
            Posicion = inscripcion.Posicion
        };
    }

    public static EquinoCampeonatoDetalleResponse ToDetalle(EquinoCampeonato inscripcion)
    {
        return new EquinoCampeonatoDetalleResponse
        {
            EquinoCampeonatoId = inscripcion.EquinoCampeonatoId,
            EquinoId = inscripcion.EquinoId,
            EquinoNombre = inscripcion.Equino.Nombre,
            CampeonatoId = inscripcion.CampeonatoId,
            CampeonatoNombre = inscripcion.Campeonato.Nombre,
            FechaCampeonato = inscripcion.Campeonato.FechaCampeonato,
            CampeonatoUbicacion = inscripcion.Campeonato.Ubicacion,
            CampeonatoDescripcion = inscripcion.Campeonato.Descripcion,
            CampeonatoNivel = inscripcion.Campeonato.Nivel,
            CategoriaId = inscripcion.CategoriaId,
            CategoriaDescripcion = inscripcion.Categoria.Descripcion,
            Resultado = inscripcion.Resultado,
            Puntaje = inscripcion.Puntaje,
            Posicion = inscripcion.Posicion
        };
    }

    public static void ApplyUpdate(EquinoCampeonato inscripcion, UpdateEquinoCampeonatoRequest request)
    {
        if (request.Resultado.HasValue)
        {
            inscripcion.Resultado = ResultadosCampeonato.Normalizar(request.Resultado.Value!);
        }

        if (request.Puntaje.HasValue)
        {
            inscripcion.Puntaje = request.Puntaje.Value;
        }

        if (request.Posicion.HasValue)
        {
            inscripcion.Posicion = request.Posicion.Value;
        }

        if (request.CategoriaId.HasValue)
        {
            inscripcion.CategoriaId = request.CategoriaId.Value;
        }
    }
}
