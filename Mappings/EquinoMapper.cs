using HorsePedigree_2026.DTOs.Equinos;
using HorsePedigree_2026.Entities;

namespace HorsePedigree_2026.Mappings;

public static class EquinoMapper
{
    public static Equino ToEntity(CreateEquinoRequest request)
    {
        return new Equino
        {
            Nombre = request.Nombre.Trim(),
            TipoDeSangre = request.TipoDeSangre,
            EstadoId = request.EstadoId,
            FechaDeNacimiento = request.FechaDeNacimiento,
            CriaderoId = request.CriaderoId,
            Descripcion = request.Descripcion,
            Sexo = request.Sexo,
            ChipId = request.ChipId,
            Capon = request.Capon,
            Mular = request.Mular,
            EnCompetencia = request.EnCompetencia,
            TipoDePasoId = request.TipoDePasoId,
            PropietarioId = request.PropietarioId,
            PadreId = request.PadreId,
            MadreId = request.MadreId
        };
    }

    public static void ApplyUpdate(Equino equino, UpdateEquinoRequest request)
    {
        if (request.Nombre.HasValue)
        {
            equino.Nombre = request.Nombre.Value!.Trim();
        }

        if (request.TipoDeSangre.HasValue)
        {
            equino.TipoDeSangre = request.TipoDeSangre.Value;
        }

        if (request.EstadoId.HasValue)
        {
            equino.EstadoId = request.EstadoId.Value;
        }

        if (request.FechaDeNacimiento.HasValue)
        {
            equino.FechaDeNacimiento = request.FechaDeNacimiento.Value;
        }

        if (request.CriaderoId.HasValue)
        {
            equino.CriaderoId = request.CriaderoId.Value;
        }

        if (request.Descripcion.HasValue)
        {
            equino.Descripcion = request.Descripcion.Value;
        }

        if (request.Sexo.HasValue)
        {
            equino.Sexo = request.Sexo.Value;
        }

        if (request.ChipId.HasValue)
        {
            equino.ChipId = request.ChipId.Value;
        }

        if (request.Capon.HasValue)
        {
            equino.Capon = request.Capon.Value;
        }

        if (request.Mular.HasValue)
        {
            equino.Mular = request.Mular.Value;
        }

        if (request.EnCompetencia.HasValue)
        {
            equino.EnCompetencia = request.EnCompetencia.Value;
        }

        if (request.TipoDePasoId.HasValue)
        {
            equino.TipoDePasoId = request.TipoDePasoId.Value;
        }

        if (request.PropietarioId.HasValue)
        {
            equino.PropietarioId = request.PropietarioId.Value;
        }

        if (request.PadreId.HasValue)
        {
            equino.PadreId = request.PadreId.Value;
        }

        if (request.MadreId.HasValue)
        {
            equino.MadreId = request.MadreId.Value;
        }
    }

    public static EquinoResponse ToResponse(Equino equino)
    {
        return new EquinoResponse
        {
            EquinoId = equino.EquinoId,
            Nombre = equino.Nombre,
            TipoDeSangre = equino.TipoDeSangre,
            EstadoId = equino.EstadoId,
            Estado = equino.Estado is null
                ? null
                : new CatalogoReferenciaDto { Id = equino.Estado.EstadoId, Descripcion = equino.Estado.Descripcion },
            FechaDeNacimiento = equino.FechaDeNacimiento,
            FechaDeFallecimiento = equino.FechaDeFallecimiento,
            CriaderoId = equino.CriaderoId,
            Criadero = equino.Criadero is null
                ? null
                : new CatalogoReferenciaDto { Id = equino.Criadero.CriaderoId, Descripcion = equino.Criadero.Nombre },
            Descripcion = equino.Descripcion,
            Sexo = equino.Sexo,
            ChipId = equino.ChipId,
            Capon = equino.Capon,
            Mular = equino.Mular,
            EnCompetencia = equino.EnCompetencia,
            TipoDePasoId = equino.TipoDePasoId,
            TipoDePaso = equino.TipoDePaso is null
                ? null
                : new CatalogoReferenciaDto { Id = equino.TipoDePaso.TipoPasoId, Descripcion = equino.TipoDePaso.Descripcion },
            PropietarioId = equino.PropietarioId,
            Propietario = equino.Propietario is null
                ? null
                : new PropietarioReferenciaDto
                {
                    Id = equino.Propietario.PropietarioId,
                    Nombre = equino.Propietario.Nombre,
                    Apellido = equino.Propietario.Apellido,
                    Alias = equino.Propietario.Alias
                },
            PadreId = equino.PadreId,
            Padre = equino.Padre is null
                ? null
                : new EquinoReferenciaDto { Id = equino.Padre.EquinoId, Nombre = equino.Padre.Nombre },
            MadreId = equino.MadreId,
            Madre = equino.Madre is null
                ? null
                : new EquinoReferenciaDto { Id = equino.Madre.EquinoId, Nombre = equino.Madre.Nombre },
            FechaDeCreacion = equino.FechaDeCreacion,
            FechaDeActualizacion = equino.FechaDeActualizacion
        };
    }

    public static EquinoGenealogiaResponse ToGenealogia(Equino equino)
    {
        return new EquinoGenealogiaResponse
        {
            EquinoId = equino.EquinoId,
            Nombre = equino.Nombre,
            Padre = ToGenealogiaAncestro(equino.Padre),
            Madre = ToGenealogiaAncestro(equino.Madre)
        };
    }

    private static EquinoGenealogiaAncestroResponse? ToGenealogiaAncestro(Equino? ancestro)
    {
        if (ancestro is null)
        {
            return null;
        }

        return new EquinoGenealogiaAncestroResponse
        {
            EquinoId = ancestro.EquinoId,
            Nombre = ancestro.Nombre,
            Padre = ToReferencia(ancestro.Padre),
            Madre = ToReferencia(ancestro.Madre)
        };
    }

    private static EquinoReferenciaDto? ToReferencia(Equino? equino)
    {
        if (equino is null)
        {
            return null;
        }

        return new EquinoReferenciaDto { Id = equino.EquinoId, Nombre = equino.Nombre };
    }

    public static EquinoListItemResponse ToListItem(Equino equino)
    {
        return new EquinoListItemResponse
        {
            EquinoId = equino.EquinoId,
            Nombre = equino.Nombre,
            TipoDeSangre = equino.TipoDeSangre,
            EstadoId = equino.EstadoId,
            EstadoDescripcion = equino.Estado?.Descripcion,
            PropietarioId = equino.PropietarioId,
            PropietarioNombreCompleto = equino.Propietario is null
                ? null
                : $"{equino.Propietario.Nombre} {equino.Propietario.Apellido}".Trim(),
            TipoDePasoId = equino.TipoDePasoId,
            TipoDePasoDescripcion = equino.TipoDePaso?.Descripcion,
            FechaDeFallecimiento = equino.FechaDeFallecimiento,
            FechaDeCreacion = equino.FechaDeCreacion
        };
    }
}
