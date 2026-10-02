using System.ComponentModel.DataAnnotations;

namespace TALLERMECANICO.CORE.Core.DTOs;

public sealed record ClienteDto(int Id, string Paterno, string? Materno, string Nombres, string Correo, string Telefono);
public sealed record ClienteRequest([property: Required, MaxLength(80)] string Paterno, [property: MaxLength(80)] string? Materno, [property: Required, MaxLength(120)] string Nombres, [property: Required, EmailAddress, MaxLength(254)] string Correo, [property: Required, MaxLength(20)] string Telefono);
public sealed record MecanicoDto(int Id, string Documento, string Paterno, string? Materno, string Nombres, string Telefono, string Especialidad, bool Activo);
public sealed record MecanicoRequest([property: Required, MaxLength(20)] string Documento, [property: Required, MaxLength(80)] string Paterno, [property: MaxLength(80)] string? Materno, [property: Required, MaxLength(120)] string Nombres, [property: Required, MaxLength(20)] string Telefono, [property: Required, MaxLength(100)] string Especialidad, bool Activo = true);
public sealed record VehiculoDto(int Id, string Placa, string Marca, string Modelo, short Anio, int ClienteId);
public sealed record VehiculoRequest([property: Required, MaxLength(15)] string Placa, [property: Required, MaxLength(60)] string Marca, [property: Required, MaxLength(80)] string Modelo, [property: Range(1900, 2100)] short Anio, int ClienteId);
public sealed record RepuestoDto(int Id, string Codigo, string Nombre, string? Marca, decimal PrecioVenta, int Stock, bool Activo);
public sealed record RepuestoRequest([property: Required, MaxLength(30)] string Codigo, [property: Required, MaxLength(120)] string Nombre, [property: MaxLength(60)] string? Marca, [property: Range(0, double.MaxValue)] decimal PrecioVenta, [property: Range(0, int.MaxValue)] int Stock, bool Activo = true);
public sealed record PagoDto(int Id, int OrdenServicioId, DateTime FechaPago, decimal Monto, string MetodoPago, string? NumeroOperacion, string Estado);
public sealed record PagoRequest(int OrdenServicioId, [property: Range(0, double.MaxValue)] decimal Monto, [property: Required, MaxLength(20)] string MetodoPago, [property: MaxLength(100)] string? NumeroOperacion, [property: Required, MaxLength(15)] string Estado = "Confirmado");
