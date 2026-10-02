using TALLERMECANICO.CORE.Core.DTOs;

namespace TALLERMECANICO.CORE.Core.Interfaces;

public interface IOrdenServicioService
{
    Task<IReadOnlyList<OrdenServicioDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrdenServicioDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<OrdenServicioDto> CreateAsync(OrdenServicioRequest request, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(int id, OrdenServicioRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
