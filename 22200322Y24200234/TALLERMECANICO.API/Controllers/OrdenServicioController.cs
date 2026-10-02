using Microsoft.AspNetCore.Mvc;
using TALLERMECANICO.CORE.Core.DTOs;
using TALLERMECANICO.CORE.Core.Interfaces;

namespace TALLERMECANICO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdenServicioController(IOrdenServicioService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrdenServicioDto>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrdenServicioDto>> GetById(int id, CancellationToken cancellationToken) =>
        (await service.GetByIdAsync(id, cancellationToken)) is { } entity ? Ok(entity) : NotFound();

    [HttpPost]
    public async Task<ActionResult<OrdenServicioDto>> Create(OrdenServicioRequest request, CancellationToken cancellationToken)
    {
        var entity = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, OrdenServicioRequest request, CancellationToken cancellationToken) =>
        await service.UpdateAsync(id, request, cancellationToken) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
        await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
