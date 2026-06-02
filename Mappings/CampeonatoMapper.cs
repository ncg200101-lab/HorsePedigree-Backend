using HorsePedigree_2026.DTOs.Campeonatos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Mappings;

public static class CampeonatoMapper
{
    public static Campeonato ToEntity(CreateCampeonatoRequest request)
    {
        return new Campeonato
        {
            Nombre = request.Nombre.Trim(),
            FechaCampeonato = request.FechaCampeonato,
            Ubicacion = request.Ubicacion.Trim(),
            Descripcion = string.IsNullOrWhiteSpace(request.Descripcion)
                ? null
                : request.Descripcion.Trim(),
            Nivel = request.Nivel.Trim()
        };
    }

    public static CampeonatoResponse ToResponse(Campeonato campeonato)
    {
        return new CampeonatoResponse
        {
            CampeonatoId = campeonato.CampeonatoId,
            Nombre = campeonato.Nombre,
            FechaCampeonato = campeonato.FechaCampeonato,
            Ubicacion = campeonato.Ubicacion,
            Descripcion = campeonato.Descripcion,
            Nivel = campeonato.Nivel
        };
    }

    public static void ApplyUpdate(Campeonato campeonato, UpdateCampeonatoRequest request)
    {
        if (request.Nombre.HasValue)
        {
            campeonato.Nombre = request.Nombre.Value!.Trim();
        }

        if (request.FechaCampeonato.HasValue)
        {
            campeonato.FechaCampeonato = request.FechaCampeonato.Value;
        }

        if (request.Ubicacion.HasValue)
        {
            campeonato.Ubicacion = request.Ubicacion.Value!.Trim();
        }

        if (request.Descripcion.HasValue)
        {
            campeonato.Descripcion = string.IsNullOrWhiteSpace(request.Descripcion.Value)
                ? null
                : request.Descripcion.Value.Trim();
        }

        if (request.Nivel.HasValue)
        {
            campeonato.Nivel = request.Nivel.Value!.Trim();
        }
    }
}
