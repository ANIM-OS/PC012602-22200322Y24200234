using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALLERMECANICO.CORE.Core.Entities;
using TALLERMECANICO.CORE.Infrastructure.Data;

namespace TALLERMECANICO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TipoServicioController(TallerMecanicoDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoServicio>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await db.TipoServicios.AsNoTracking().OrderBy(x => x.Id).ToListAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TipoServicio>> GetById(int id, CancellationToken cancellationToken)
    {
        var entity = await db.TipoServicios.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        return entity is null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<TipoServicio>> Create(TipoServicio entity, CancellationToken cancellationToken)
    {
        entity.Id = 0;
        db.TipoServicios.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TipoServicio entity, CancellationToken cancellationToken)
    {
        if (id != entity.Id) return BadRequest("El id de la ruta no coincide con el id de la entidad.");
        if (!await db.TipoServicios.AnyAsync(x => x.Id == id, cancellationToken)) return NotFound();
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var entity = await db.TipoServicios.FindAsync([id], cancellationToken);
        if (entity is null) return NotFound();
        db.TipoServicios.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}
