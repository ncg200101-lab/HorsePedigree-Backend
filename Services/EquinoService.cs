using HorsePedigree_2026.Data;

using HorsePedigree_2026.DTOs.Equinos;

using HorsePedigree_2026.Entities;

using HorsePedigree_2026.Exceptions;

using HorsePedigree_2026.Helpers;

using HorsePedigree_2026.Mappings;

using HorsePedigree_2026.Repositories;

using Microsoft.EntityFrameworkCore;



namespace HorsePedigree_2026.Services;



public class EquinoService : IEquinoService

{

    private readonly IEquinoRepository _equinoRepository;

    private readonly ApplicationDbContext _context;



    public EquinoService(IEquinoRepository equinoRepository, ApplicationDbContext context)

    {

        _equinoRepository = equinoRepository;

        _context = context;

    }



    public async Task<EquinoResponse> CreateAsync(CreateEquinoRequest request, CancellationToken cancellationToken = default)

    {

        await ValidarReferenciasAsync(

            request.EstadoId,

            request.PropietarioId,

            request.TipoDePasoId,

            request.CriaderoId,

            request.PadreId,

            request.MadreId,

            equinoIdExcluir: null,

            cancellationToken);



        await ValidarChipIdUnicoAsync(request.ChipId, equinoIdExcluir: null, cancellationToken);



        var equino = EquinoMapper.ToEntity(request);

        await _equinoRepository.AddAsync(equino, cancellationToken);



        var creado = await _equinoRepository.GetByIdWithRelationsAsync(equino.EquinoId, cancellationToken);

        return EquinoMapper.ToResponse(creado!);

    }



    public async Task<EquinoResponse> GetByIdAsync(long id, CancellationToken cancellationToken = default)

    {

        var equino = await _equinoRepository.GetByIdWithRelationsAsync(id, cancellationToken);

        if (equino is null)

        {

            throw new NotFoundException(nameof(Equino), id);

        }



        return EquinoMapper.ToResponse(equino);

    }



    public async Task<EquinoGenealogiaResponse> GetGenealogiaAsync(long id, CancellationToken cancellationToken = default)

    {

        var equino = await _equinoRepository.GetByIdForGenealogiaAsync(id, cancellationToken);

        if (equino is null)

        {

            throw new NotFoundException(nameof(Equino), id);

        }



        return EquinoMapper.ToGenealogia(equino);

    }



    public async Task<EquinoResponse> UpdateAsync(long id, UpdateEquinoRequest request, CancellationToken cancellationToken = default)

    {

        var equino = await _equinoRepository.GetByIdForUpdateAsync(id, cancellationToken);

        if (equino is null)

        {

            throw new NotFoundException(nameof(Equino), id);

        }



        await ValidarReferenciasParcialAsync(request, equinoIdExcluir: id, cancellationToken);



        if (request.ChipId.HasValue)

        {

            await ValidarChipIdUnicoAsync(request.ChipId.Value, equinoIdExcluir: id, cancellationToken);

        }



        EquinoMapper.ApplyUpdate(equino, request);



        if (request.EstadoId is { HasValue: true, Value: not null })

        {

            var estado = await _context.Estados

                .AsNoTracking()

                .FirstOrDefaultAsync(e => e.EstadoId == request.EstadoId.Value!.Value, cancellationToken);



            if (estado is not null)

            {

                var fechaDeFallecimiento = request.FechaDeFallecimiento.HasValue

                    ? request.FechaDeFallecimiento.Value

                    : null;

                AplicarReglasEstado(equino, estado, fechaDeFallecimiento);

            }

        }

        else if (request.FechaDeFallecimiento.HasValue)

        {

            equino.FechaDeFallecimiento = request.FechaDeFallecimiento.Value;

        }



        await _equinoRepository.UpdateAsync(equino, cancellationToken);



        var actualizado = await _equinoRepository.GetByIdWithRelationsAsync(id, cancellationToken);

        return EquinoMapper.ToResponse(actualizado!);

    }



    public async Task<EquinoResponse> CambiarEstadoAsync(

        long id,

        CambiarEstadoEquinoRequest request,

        CancellationToken cancellationToken = default)

    {

        var equino = await _equinoRepository.GetByIdForUpdateAsync(id, cancellationToken);

        if (equino is null)

        {

            throw new NotFoundException(nameof(Equino), id);

        }



        var estado = await _context.Estados

            .AsNoTracking()

            .FirstOrDefaultAsync(e => e.EstadoId == request.EstadoId, cancellationToken);



        if (estado is null)

        {

            throw new BusinessException($"No existe un estado con id {request.EstadoId}.");

        }



        AplicarReglasEstado(equino, estado, request.FechaDeFallecimiento);

        equino.EstadoId = request.EstadoId;



        await _equinoRepository.UpdateAsync(equino, cancellationToken);



        var actualizado = await _equinoRepository.GetByIdWithRelationsAsync(id, cancellationToken);

        return EquinoMapper.ToResponse(actualizado!);

    }



    public async Task<IReadOnlyList<EquinoListItemResponse>> GetAllAsync(

        EquinoFiltrosQuery filtros,

        CancellationToken cancellationToken = default)

    {

        if (filtros.PropietarioId.HasValue &&

            !await _context.Propietarios.AnyAsync(p => p.PropietarioId == filtros.PropietarioId.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un propietario con id {filtros.PropietarioId.Value}.");

        }



        if (filtros.EstadoId.HasValue &&

            !await _context.Estados.AnyAsync(e => e.EstadoId == filtros.EstadoId.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un estado con id {filtros.EstadoId.Value}.");

        }



        var equinos = await _equinoRepository.BuscarAsync(filtros, cancellationToken);

        return equinos.Select(EquinoMapper.ToListItem).ToList();

    }



    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)

    {

        var equino = await _equinoRepository.GetByIdAsync(id, cancellationToken);

        if (equino is null)

        {

            throw new NotFoundException(nameof(Equino), id);

        }



        var tieneCampeonatos = await _context.EquinoCampeonatos

            .AnyAsync(ec => ec.EquinoId == id, cancellationToken);



        if (tieneCampeonatos)

        {

            throw new BusinessException(

                "No se puede eliminar el equino porque tiene participaciones registradas en campeonatos.");

        }



        var esAncestro = await _context.Equinos

            .AnyAsync(e => e.PadreId == id || e.MadreId == id, cancellationToken);



        if (esAncestro)

        {

            throw new BusinessException(

                "No se puede eliminar el equino porque otros registros lo tienen como padre o madre.");

        }



        await _equinoRepository.DeleteAsync(equino, cancellationToken);

    }



    private async Task ValidarReferenciasParcialAsync(

        UpdateEquinoRequest request,

        long? equinoIdExcluir,

        CancellationToken cancellationToken)

    {

        if (request.EstadoId is { HasValue: true, Value: not null } &&

            !await _context.Estados.AnyAsync(e => e.EstadoId == request.EstadoId.Value!.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un estado con id {request.EstadoId.Value!.Value}.");

        }



        if (request.PropietarioId is { HasValue: true, Value: not null } &&

            !await _context.Propietarios.AnyAsync(p => p.PropietarioId == request.PropietarioId.Value!.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un propietario con id {request.PropietarioId.Value!.Value}.");

        }



        if (request.TipoDePasoId is { HasValue: true, Value: not null } &&

            !await _context.TiposDePaso.AnyAsync(t => t.TipoPasoId == request.TipoDePasoId.Value!.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un tipo de paso con id {request.TipoDePasoId.Value!.Value}.");

        }



        if (request.CriaderoId is { HasValue: true, Value: not null } &&

            !await _context.Criaderos.AnyAsync(c => c.CriaderoId == request.CriaderoId.Value!.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un criadero con id {request.CriaderoId.Value!.Value}.");

        }



        if (request.PadreId is { HasValue: true, Value: not null })

        {

            var padreId = request.PadreId.Value!.Value;

            if (equinoIdExcluir.HasValue && padreId == equinoIdExcluir.Value)

            {

                throw new BusinessException("Un equino no puede ser su propio padre.");

            }



            if (!await _context.Equinos.AnyAsync(e => e.EquinoId == padreId, cancellationToken))

            {

                throw new BusinessException($"No existe un equino (padre) con id {padreId}.");

            }

        }



        if (request.MadreId is { HasValue: true, Value: not null })

        {

            var madreId = request.MadreId.Value!.Value;

            if (equinoIdExcluir.HasValue && madreId == equinoIdExcluir.Value)

            {

                throw new BusinessException("Un equino no puede ser su propia madre.");

            }



            if (!await _context.Equinos.AnyAsync(e => e.EquinoId == madreId, cancellationToken))

            {

                throw new BusinessException($"No existe un equino (madre) con id {madreId}.");

            }

        }

    }



    private static void AplicarReglasEstado(Equino equino, Estado estado, DateOnly? fechaDeFallecimientoSolicitada)

    {

        var nuevoEsFallecido = EquinoEstadoHelper.EsEstadoFallecido(estado.Descripcion);

        var anteriorEsFallecido = EquinoEstadoHelper.EsEstadoFallecido(equino.Estado?.Descripcion);



        if (nuevoEsFallecido)

        {

            equino.FechaDeFallecimiento = fechaDeFallecimientoSolicitada ?? DateOnly.FromDateTime(DateTime.UtcNow);

            return;

        }



        if (anteriorEsFallecido)

        {

            equino.FechaDeFallecimiento = null;

        }

    }



    private async Task ValidarChipIdUnicoAsync(

        string? chipId,

        long? equinoIdExcluir,

        CancellationToken cancellationToken)

    {

        if (string.IsNullOrWhiteSpace(chipId))

        {

            return;

        }



        var normalizado = chipId.Trim();

        var duplicado = await _context.Equinos.AnyAsync(

            e => e.ChipId == normalizado && (!equinoIdExcluir.HasValue || e.EquinoId != equinoIdExcluir.Value),

            cancellationToken);



        if (duplicado)

        {

            throw new BusinessException($"Ya existe un equino con chip_id '{normalizado}'.");

        }

    }



    private async Task ValidarReferenciasAsync(

        long? estadoId,

        long? propietarioId,

        long? tipoDePasoId,

        long? criaderoId,

        long? padreId,

        long? madreId,

        long? equinoIdExcluir,

        CancellationToken cancellationToken)

    {

        if (estadoId.HasValue && !await _context.Estados.AnyAsync(e => e.EstadoId == estadoId.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un estado con id {estadoId.Value}.");

        }



        if (propietarioId.HasValue && !await _context.Propietarios.AnyAsync(p => p.PropietarioId == propietarioId.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un propietario con id {propietarioId.Value}.");

        }



        if (tipoDePasoId.HasValue && !await _context.TiposDePaso.AnyAsync(t => t.TipoPasoId == tipoDePasoId.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un tipo de paso con id {tipoDePasoId.Value}.");

        }



        if (criaderoId.HasValue && !await _context.Criaderos.AnyAsync(c => c.CriaderoId == criaderoId.Value, cancellationToken))

        {

            throw new BusinessException($"No existe un criadero con id {criaderoId.Value}.");

        }



        if (padreId.HasValue)

        {

            if (equinoIdExcluir.HasValue && padreId.Value == equinoIdExcluir.Value)

            {

                throw new BusinessException("Un equino no puede ser su propio padre.");

            }



            if (!await _context.Equinos.AnyAsync(e => e.EquinoId == padreId.Value, cancellationToken))

            {

                throw new BusinessException($"No existe un equino (padre) con id {padreId.Value}.");

            }

        }



        if (madreId.HasValue)

        {

            if (equinoIdExcluir.HasValue && madreId.Value == equinoIdExcluir.Value)

            {

                throw new BusinessException("Un equino no puede ser su propia madre.");

            }



            if (!await _context.Equinos.AnyAsync(e => e.EquinoId == madreId.Value, cancellationToken))

            {

                throw new BusinessException($"No existe un equino (madre) con id {madreId.Value}.");

            }

        }

    }

}

