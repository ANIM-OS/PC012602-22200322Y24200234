using Microsoft.EntityFrameworkCore;
using TALLERMECANICO.CORE.Core.Entities;
using TALLERMECANICO.CORE.Core.Interfaces;
using TALLERMECANICO.CORE.Infrastructure.Data;

namespace TALLERMECANICO.CORE.Infrastructure.Repositories;

public sealed class OrdenServicioRepository(TallerMecanicoDbContext db) : IOrdenServicioRepository
{
    public async Task<IReadOnlyList<OrdenServicio>> GetAllAsync(CancellationToken cancellationToken) =>
        await Query().OrderBy(x => x.Id).ToListAsync(cancellationToken);

    public Task<OrdenServicio?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        Query().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(OrdenServicio entity, CancellationToken cancellationToken) =>
        await db.OrdenServicios.AddAsync(entity, cancellationToken);

    public void Update(OrdenServicio entity) => db.OrdenServicios.Update(entity);

    public void Delete(OrdenServicio entity) => db.OrdenServicios.Remove(entity);

    public Task SaveChangesAsync(CancellationToken cancellationToken) => db.SaveChangesAsync(cancellationToken);

    private IQueryable<OrdenServicio> Query() => db.OrdenServicios
        .AsNoTracking()
        .AsSplitQuery()
        .Include(x => x.OrdenMecanicos)
        .Include(x => x.OrdenRepuestos)
        .Include(x => x.Pagos);
}
