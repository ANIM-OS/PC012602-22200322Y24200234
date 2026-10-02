using Microsoft.EntityFrameworkCore;
using TALLERMECANICO.CORE.Core.DTOs;
using TALLERMECANICO.CORE.Core.Entities;
using TALLERMECANICO.CORE.Core.Interfaces;
using TALLERMECANICO.CORE.Infrastructure.Data;

namespace TALLERMECANICO.CORE.Core.Services;

public sealed class TallerMecanicoService(TallerMecanicoDbContext db) : ITallerMecanicoService
{
    public async Task<IReadOnlyList<ClienteDto>> GetClientesAsync(CancellationToken ct) => await db.Clientes.AsNoTracking().OrderBy(x => x.Id).Select(x => new ClienteDto(x.Id, x.Paterno, x.Materno, x.Nombres, x.Correo, x.Telefono)).ToListAsync(ct);
    public async Task<ClienteDto?> GetClienteAsync(int id, CancellationToken ct) => await db.Clientes.AsNoTracking().Where(x => x.Id == id).Select(x => new ClienteDto(x.Id, x.Paterno, x.Materno, x.Nombres, x.Correo, x.Telefono)).SingleOrDefaultAsync(ct);
    public async Task<ClienteDto> CreateClienteAsync(ClienteRequest r, CancellationToken ct) { var e = new Cliente { Paterno = r.Paterno, Materno = r.Materno, Nombres = r.Nombres, Correo = r.Correo, Telefono = r.Telefono }; db.Clientes.Add(e); await db.SaveChangesAsync(ct); return ClienteToDto(e); }
    public async Task<bool> UpdateClienteAsync(int id, ClienteRequest r, CancellationToken ct) { var e = await db.Clientes.FindAsync([id], ct); if (e is null) return false; e.Paterno = r.Paterno; e.Materno = r.Materno; e.Nombres = r.Nombres; e.Correo = r.Correo; e.Telefono = r.Telefono; await db.SaveChangesAsync(ct); return true; }
    public async Task<bool> DeleteClienteAsync(int id, CancellationToken ct) => await RemoveAsync(db.Clientes, id, ct);

    public async Task<IReadOnlyList<MecanicoDto>> GetMecanicosAsync(CancellationToken ct) => await db.Mecanicos.AsNoTracking().OrderBy(x => x.Id).Select(x => new MecanicoDto(x.Id, x.Documento, x.Paterno, x.Materno, x.Nombres, x.Telefono, x.Especialidad, x.Activo)).ToListAsync(ct);
    public async Task<MecanicoDto?> GetMecanicoAsync(int id, CancellationToken ct) => await db.Mecanicos.AsNoTracking().Where(x => x.Id == id).Select(x => new MecanicoDto(x.Id, x.Documento, x.Paterno, x.Materno, x.Nombres, x.Telefono, x.Especialidad, x.Activo)).SingleOrDefaultAsync(ct);
    public async Task<MecanicoDto> CreateMecanicoAsync(MecanicoRequest r, CancellationToken ct) { var e = new Mecanico { Documento = r.Documento, Paterno = r.Paterno, Materno = r.Materno, Nombres = r.Nombres, Telefono = r.Telefono, Especialidad = r.Especialidad, Activo = r.Activo }; db.Mecanicos.Add(e); await db.SaveChangesAsync(ct); return MecanicoToDto(e); }
    public async Task<bool> UpdateMecanicoAsync(int id, MecanicoRequest r, CancellationToken ct) { var e = await db.Mecanicos.FindAsync([id], ct); if (e is null) return false; e.Documento = r.Documento; e.Paterno = r.Paterno; e.Materno = r.Materno; e.Nombres = r.Nombres; e.Telefono = r.Telefono; e.Especialidad = r.Especialidad; e.Activo = r.Activo; await db.SaveChangesAsync(ct); return true; }
    public async Task<bool> DeleteMecanicoAsync(int id, CancellationToken ct) => await RemoveAsync(db.Mecanicos, id, ct);

    public async Task<IReadOnlyList<VehiculoDto>> GetVehiculosAsync(CancellationToken ct) => await db.Vehiculos.AsNoTracking().OrderBy(x => x.Id).Select(x => new VehiculoDto(x.Id, x.Placa, x.Marca, x.Modelo, x.Anio, x.ClienteId)).ToListAsync(ct);
    public async Task<VehiculoDto?> GetVehiculoAsync(int id, CancellationToken ct) => await db.Vehiculos.AsNoTracking().Where(x => x.Id == id).Select(x => new VehiculoDto(x.Id, x.Placa, x.Marca, x.Modelo, x.Anio, x.ClienteId)).SingleOrDefaultAsync(ct);
    public async Task<VehiculoDto> CreateVehiculoAsync(VehiculoRequest r, CancellationToken ct) { var e = new Vehiculo { Placa = r.Placa, Marca = r.Marca, Modelo = r.Modelo, Anio = r.Anio, ClienteId = r.ClienteId }; db.Vehiculos.Add(e); await db.SaveChangesAsync(ct); return VehiculoToDto(e); }
    public async Task<bool> UpdateVehiculoAsync(int id, VehiculoRequest r, CancellationToken ct) { var e = await db.Vehiculos.FindAsync([id], ct); if (e is null) return false; e.Placa = r.Placa; e.Marca = r.Marca; e.Modelo = r.Modelo; e.Anio = r.Anio; e.ClienteId = r.ClienteId; await db.SaveChangesAsync(ct); return true; }
    public async Task<bool> DeleteVehiculoAsync(int id, CancellationToken ct) => await RemoveAsync(db.Vehiculos, id, ct);

    public async Task<IReadOnlyList<RepuestoDto>> GetRepuestosAsync(CancellationToken ct) => await db.Repuestos.AsNoTracking().OrderBy(x => x.Id).Select(x => new RepuestoDto(x.Id, x.Codigo, x.Nombre, x.Marca, x.PrecioVenta, x.Stock, x.Activo)).ToListAsync(ct);
    public async Task<RepuestoDto> CreateRepuestoAsync(RepuestoRequest r, CancellationToken ct) { var e = new Repuesto { Codigo = r.Codigo, Nombre = r.Nombre, Marca = r.Marca, PrecioVenta = r.PrecioVenta, Stock = r.Stock, Activo = r.Activo }; db.Repuestos.Add(e); await db.SaveChangesAsync(ct); return RepuestoToDto(e); }
    public async Task<bool> UpdateRepuestoAsync(int id, RepuestoRequest r, CancellationToken ct) { var e = await db.Repuestos.FindAsync([id], ct); if (e is null) return false; e.Codigo = r.Codigo; e.Nombre = r.Nombre; e.Marca = r.Marca; e.PrecioVenta = r.PrecioVenta; e.Stock = r.Stock; e.Activo = r.Activo; await db.SaveChangesAsync(ct); return true; }

    public async Task<IReadOnlyList<OrdenMecanicoDto>> GetAsignacionesAsync(CancellationToken ct) => await db.OrdenMecanicos.AsNoTracking().OrderBy(x => x.Id).Select(x => new OrdenMecanicoDto(x.Id, x.OrdenServicioId, x.MecanicoId, x.FechaAsignacion, x.Observaciones)).ToListAsync(ct);
    public async Task<OrdenMecanicoDto> CreateAsignacionAsync(OrdenMecanicoRequest r, CancellationToken ct) { var e = new OrdenMecanico { OrdenServicioId = r.OrdenServicioId, MecanicoId = r.MecanicoId, Observaciones = r.Observaciones, FechaAsignacion = DateTime.UtcNow }; db.OrdenMecanicos.Add(e); await db.SaveChangesAsync(ct); return new(e.Id, e.OrdenServicioId, e.MecanicoId, e.FechaAsignacion, e.Observaciones); }
    public async Task<IReadOnlyList<OrdenRepuestoDto>> GetRepuestosOrdenAsync(CancellationToken ct) => await db.OrdenRepuestos.AsNoTracking().OrderBy(x => x.Id).Select(x => new OrdenRepuestoDto(x.Id, x.OrdenServicioId, x.RepuestoId, x.Cantidad, x.PrecioUnitario, x.Subtotal)).ToListAsync(ct);
    public async Task<OrdenRepuestoDto> CreateRepuestoOrdenAsync(OrdenRepuestoRequest r, CancellationToken ct) { var e = new OrdenRepuesto { OrdenServicioId = r.OrdenServicioId, RepuestoId = r.RepuestoId, Cantidad = r.Cantidad, PrecioUnitario = r.PrecioUnitario }; db.OrdenRepuestos.Add(e); await db.SaveChangesAsync(ct); return new(e.Id, e.OrdenServicioId, e.RepuestoId, e.Cantidad, e.PrecioUnitario, e.Subtotal); }
    public async Task<IReadOnlyList<PagoDto>> GetPagosAsync(CancellationToken ct) => await db.Pagos.AsNoTracking().OrderBy(x => x.Id).Select(x => new PagoDto(x.Id, x.OrdenServicioId, x.FechaPago, x.Monto, x.MetodoPago, x.NumeroOperacion, x.Estado)).ToListAsync(ct);
    public async Task<PagoDto> CreatePagoAsync(PagoRequest r, CancellationToken ct) { var e = new Pago { OrdenServicioId = r.OrdenServicioId, Monto = r.Monto, MetodoPago = r.MetodoPago, NumeroOperacion = r.NumeroOperacion, Estado = r.Estado, FechaPago = DateTime.UtcNow }; db.Pagos.Add(e); await db.SaveChangesAsync(ct); return new(e.Id, e.OrdenServicioId, e.FechaPago, e.Monto, e.MetodoPago, e.NumeroOperacion, e.Estado); }

    private static ClienteDto ClienteToDto(Cliente x) => new(x.Id, x.Paterno, x.Materno, x.Nombres, x.Correo, x.Telefono);
    private static MecanicoDto MecanicoToDto(Mecanico x) => new(x.Id, x.Documento, x.Paterno, x.Materno, x.Nombres, x.Telefono, x.Especialidad, x.Activo);
    private static VehiculoDto VehiculoToDto(Vehiculo x) => new(x.Id, x.Placa, x.Marca, x.Modelo, x.Anio, x.ClienteId);
    private static RepuestoDto RepuestoToDto(Repuesto x) => new(x.Id, x.Codigo, x.Nombre, x.Marca, x.PrecioVenta, x.Stock, x.Activo);
    private async Task<bool> RemoveAsync<TEntity>(DbSet<TEntity> set, int id, CancellationToken ct) where TEntity : class { var entity = await set.FindAsync([id], ct); if (entity is null) return false; set.Remove(entity); await db.SaveChangesAsync(ct); return true; }
}
