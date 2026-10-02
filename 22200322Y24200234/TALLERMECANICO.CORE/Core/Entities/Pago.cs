using System;
using System.Collections.Generic;

namespace TALLERMECANICO.CORE.Core.Entities;

public partial class Pago
{
    public int Id { get; set; }

    public int OrdenServicioId { get; set; }

    public DateTime FechaPago { get; set; }

    public decimal Monto { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string? NumeroOperacion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual OrdenServicio OrdenServicio { get; set; } = null!;
}
