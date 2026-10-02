using System.ComponentModel.DataAnnotations;

namespace TALLERMECANICO.CORE.Core.DTOs;

public sealed record OrdenMecanicoDto(int Id, int OrdenServicioId, int MecanicoId, DateTime FechaAsignacion, string? Observaciones);
public sealed record OrdenMecanicoRequest(int OrdenServicioId, int MecanicoId, [property: MaxLength(500)] string? Observaciones);
public sealed record OrdenRepuestoDto(int Id, int OrdenServicioId, int RepuestoId, int Cantidad, decimal PrecioUnitario, decimal? Subtotal);
public sealed record OrdenRepuestoRequest(int OrdenServicioId, int RepuestoId, [property: Range(1, int.MaxValue)] int Cantidad, [property: Range(0, double.MaxValue)] decimal PrecioUnitario);
public sealed record OrdenServicioDto(int Id, DateTime FechaIngreso, string DescripcionProblema, decimal CostoEstimado, string Estado, int VehiculoId, int TipoServicioId, IReadOnlyList<OrdenMecanicoDto> Mecanicos, IReadOnlyList<OrdenRepuestoDto> Repuestos, IReadOnlyList<PagoDto> Pagos);
public sealed record OrdenServicioRequest(int VehiculoId, int TipoServicioId, [property: Required, MaxLength(1000)] string DescripcionProblema, [property: Range(0, double.MaxValue)] decimal CostoEstimado, [property: Required, MaxLength(20)] string Estado = "Pendiente");
