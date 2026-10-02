using TALLERMECANICO.CORE.Core.Entities;

namespace TALLERMECANICO.CORE.Core.Interfaces;

public interface IOrdenServicioRepository
{
    Task<IReadOnlyList<OrdenServicio>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrdenServicio?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(OrdenServicio entity, CancellationToken cancellationToken);
    void Update(OrdenServicio entity);
    void Delete(OrdenServicio entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
