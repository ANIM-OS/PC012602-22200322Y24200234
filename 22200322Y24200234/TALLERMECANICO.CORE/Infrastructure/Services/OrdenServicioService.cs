using TALLERMECANICO.CORE.Core.DTOs;
using TALLERMECANICO.CORE.Core.Entities;
using TALLERMECANICO.CORE.Core.Interfaces;

namespace TALLERMECANICO.CORE.Infrastructure.Services;

public sealed class OrdenServicioService(IOrdenServicioRepository repository) : IOrdenServicioService
{
    public async Task<IReadOnlyList<OrdenServicioDto>> GetAllAsync(CancellationToken cancellationToken) =>
        (await repository.GetAllAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<OrdenServicioDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<OrdenServicioDto> CreateAsync(OrdenServicioRequest request, CancellationToken cancellationToken)
    {
        var entity = new OrdenServicio
        {
            VehiculoId = request.VehiculoId,
            TipoServicioId = request.TipoServicioId,
            DescripcionProblema = request.DescripcionProblema,
            CostoEstimado = request.CostoEstimado,
            Estado = request.Estado,
            FechaIngreso = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, OrdenServicioRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        entity.VehiculoId = request.VehiculoId;
        entity.TipoServicioId = request.TipoServicioId;
        entity.DescripcionProblema = request.DescripcionProblema;
        entity.CostoEstimado = request.CostoEstimado;
        entity.Estado = request.Estado;
        repository.Update(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        repository.Delete(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static OrdenServicioDto ToDto(OrdenServicio entity) => new(
        entity.Id,
        entity.FechaIngreso,
        entity.DescripcionProblema,
        entity.CostoEstimado,
        entity.Estado,
        entity.VehiculoId,
        entity.TipoServicioId,
        entity.OrdenMecanicos.Select(x => new OrdenMecanicoDto(x.Id, x.OrdenServicioId, x.MecanicoId, x.FechaAsignacion, x.Observaciones)).ToList(),
        entity.OrdenRepuestos.Select(x => new OrdenRepuestoDto(x.Id, x.OrdenServicioId, x.RepuestoId, x.Cantidad, x.PrecioUnitario, x.Subtotal)).ToList(),
        entity.Pagos.Select(x => new PagoDto(x.Id, x.OrdenServicioId, x.FechaPago, x.Monto, x.MetodoPago, x.NumeroOperacion, x.Estado)).ToList());
}
