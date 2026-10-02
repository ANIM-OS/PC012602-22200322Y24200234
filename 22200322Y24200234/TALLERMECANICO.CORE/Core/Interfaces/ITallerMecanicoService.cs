using TALLERMECANICO.CORE.Core.DTOs;

namespace TALLERMECANICO.CORE.Core.Interfaces;

public interface ITallerMecanicoService
{
    Task<IReadOnlyList<ClienteDto>> GetClientesAsync(CancellationToken ct); Task<ClienteDto?> GetClienteAsync(int id, CancellationToken ct); Task<ClienteDto> CreateClienteAsync(ClienteRequest request, CancellationToken ct); Task<bool> UpdateClienteAsync(int id, ClienteRequest request, CancellationToken ct); Task<bool> DeleteClienteAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<MecanicoDto>> GetMecanicosAsync(CancellationToken ct); Task<MecanicoDto?> GetMecanicoAsync(int id, CancellationToken ct); Task<MecanicoDto> CreateMecanicoAsync(MecanicoRequest request, CancellationToken ct); Task<bool> UpdateMecanicoAsync(int id, MecanicoRequest request, CancellationToken ct); Task<bool> DeleteMecanicoAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<VehiculoDto>> GetVehiculosAsync(CancellationToken ct); Task<VehiculoDto?> GetVehiculoAsync(int id, CancellationToken ct); Task<VehiculoDto> CreateVehiculoAsync(VehiculoRequest request, CancellationToken ct); Task<bool> UpdateVehiculoAsync(int id, VehiculoRequest request, CancellationToken ct); Task<bool> DeleteVehiculoAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<RepuestoDto>> GetRepuestosAsync(CancellationToken ct); Task<RepuestoDto> CreateRepuestoAsync(RepuestoRequest request, CancellationToken ct); Task<bool> UpdateRepuestoAsync(int id, RepuestoRequest request, CancellationToken ct);
    Task<IReadOnlyList<OrdenMecanicoDto>> GetAsignacionesAsync(CancellationToken ct); Task<OrdenMecanicoDto> CreateAsignacionAsync(OrdenMecanicoRequest request, CancellationToken ct);
    Task<IReadOnlyList<OrdenRepuestoDto>> GetRepuestosOrdenAsync(CancellationToken ct); Task<OrdenRepuestoDto> CreateRepuestoOrdenAsync(OrdenRepuestoRequest request, CancellationToken ct);
    Task<IReadOnlyList<PagoDto>> GetPagosAsync(CancellationToken ct); Task<PagoDto> CreatePagoAsync(PagoRequest request, CancellationToken ct);
}
