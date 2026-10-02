using System.ComponentModel.DataAnnotations;

namespace TALLERMECANICO.CORE.Core.DTOs;

public sealed record TipoServicioDto(int Id, string Nombre, decimal PrecioBase);
public sealed record TipoServicioRequest([property: Required, MaxLength(100)] string Nombre, [property: Range(0, double.MaxValue)] decimal PrecioBase);
