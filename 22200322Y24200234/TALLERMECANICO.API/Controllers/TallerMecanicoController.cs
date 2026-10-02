using Microsoft.AspNetCore.Mvc;
using TALLERMECANICO.CORE.Core.DTOs;
using TALLERMECANICO.CORE.Core.Interfaces;

namespace TALLERMECANICO.API.Controllers;

[ApiController]
[Route("api")]
public sealed class TallerMecanicoController(ITallerMecanicoService service) : ControllerBase
{
    [HttpGet("clientes")] public async Task<ActionResult<IReadOnlyList<ClienteDto>>> Clientes(CancellationToken ct) => Ok(await service.GetClientesAsync(ct));
    [HttpGet("clientes/{id:int}")] public async Task<ActionResult<ClienteDto>> Cliente(int id, CancellationToken ct) => (await service.GetClienteAsync(id, ct)) is { } x ? Ok(x) : NotFound();
    [HttpPost("clientes")] public async Task<ActionResult<ClienteDto>> CreateCliente(ClienteRequest r, CancellationToken ct) => Ok(await service.CreateClienteAsync(r, ct));
    [HttpPut("clientes/{id:int}")] public async Task<IActionResult> UpdateCliente(int id, ClienteRequest r, CancellationToken ct) => await service.UpdateClienteAsync(id, r, ct) ? NoContent() : NotFound();
    [HttpDelete("clientes/{id:int}")] public async Task<IActionResult> DeleteCliente(int id, CancellationToken ct) => await service.DeleteClienteAsync(id, ct) ? NoContent() : NotFound();

    [HttpGet("mecanicos")] public async Task<ActionResult<IReadOnlyList<MecanicoDto>>> Mecanicos(CancellationToken ct) => Ok(await service.GetMecanicosAsync(ct));
    [HttpGet("mecanicos/{id:int}")] public async Task<ActionResult<MecanicoDto>> Mecanico(int id, CancellationToken ct) => (await service.GetMecanicoAsync(id, ct)) is { } x ? Ok(x) : NotFound();
    [HttpPost("mecanicos")] public async Task<ActionResult<MecanicoDto>> CreateMecanico(MecanicoRequest r, CancellationToken ct) => Ok(await service.CreateMecanicoAsync(r, ct));
    [HttpPut("mecanicos/{id:int}")] public async Task<IActionResult> UpdateMecanico(int id, MecanicoRequest r, CancellationToken ct) => await service.UpdateMecanicoAsync(id, r, ct) ? NoContent() : NotFound();
    [HttpDelete("mecanicos/{id:int}")] public async Task<IActionResult> DeleteMecanico(int id, CancellationToken ct) => await service.DeleteMecanicoAsync(id, ct) ? NoContent() : NotFound();

    [HttpGet("vehiculos")] public async Task<ActionResult<IReadOnlyList<VehiculoDto>>> Vehiculos(CancellationToken ct) => Ok(await service.GetVehiculosAsync(ct));
    [HttpGet("vehiculos/{id:int}")] public async Task<ActionResult<VehiculoDto>> Vehiculo(int id, CancellationToken ct) => (await service.GetVehiculoAsync(id, ct)) is { } x ? Ok(x) : NotFound();
    [HttpPost("vehiculos")] public async Task<ActionResult<VehiculoDto>> CreateVehiculo(VehiculoRequest r, CancellationToken ct) => Ok(await service.CreateVehiculoAsync(r, ct));
    [HttpPut("vehiculos/{id:int}")] public async Task<IActionResult> UpdateVehiculo(int id, VehiculoRequest r, CancellationToken ct) => await service.UpdateVehiculoAsync(id, r, ct) ? NoContent() : NotFound();
    [HttpDelete("vehiculos/{id:int}")] public async Task<IActionResult> DeleteVehiculo(int id, CancellationToken ct) => await service.DeleteVehiculoAsync(id, ct) ? NoContent() : NotFound();

    [HttpGet("repuestos")] public async Task<ActionResult<IReadOnlyList<RepuestoDto>>> Repuestos(CancellationToken ct) => Ok(await service.GetRepuestosAsync(ct));
    [HttpPost("repuestos")] public async Task<ActionResult<RepuestoDto>> CreateRepuesto(RepuestoRequest r, CancellationToken ct) => Ok(await service.CreateRepuestoAsync(r, ct));
    [HttpPut("repuestos/{id:int}")] public async Task<IActionResult> UpdateRepuesto(int id, RepuestoRequest r, CancellationToken ct) => await service.UpdateRepuestoAsync(id, r, ct) ? NoContent() : NotFound();

    [HttpGet("asignaciones")] public async Task<ActionResult<IReadOnlyList<OrdenMecanicoDto>>> Asignaciones(CancellationToken ct) => Ok(await service.GetAsignacionesAsync(ct));
    [HttpPost("asignaciones")] public async Task<ActionResult<OrdenMecanicoDto>> CreateAsignacion(OrdenMecanicoRequest r, CancellationToken ct) => Ok(await service.CreateAsignacionAsync(r, ct));
    [HttpGet("ordenes-repuestos")] public async Task<ActionResult<IReadOnlyList<OrdenRepuestoDto>>> OrdenesRepuestos(CancellationToken ct) => Ok(await service.GetRepuestosOrdenAsync(ct));
    [HttpPost("ordenes-repuestos")] public async Task<ActionResult<OrdenRepuestoDto>> CreateOrdenRepuesto(OrdenRepuestoRequest r, CancellationToken ct) => Ok(await service.CreateRepuestoOrdenAsync(r, ct));
    [HttpGet("pagos")] public async Task<ActionResult<IReadOnlyList<PagoDto>>> Pagos(CancellationToken ct) => Ok(await service.GetPagosAsync(ct));
    [HttpPost("pagos")] public async Task<ActionResult<PagoDto>> CreatePago(PagoRequest r, CancellationToken ct) => Ok(await service.CreatePagoAsync(r, ct));
}
