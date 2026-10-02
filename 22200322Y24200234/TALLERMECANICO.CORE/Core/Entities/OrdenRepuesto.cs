using System;
using System.Collections.Generic;

namespace TALLERMECANICO.CORE.Core.Entities;

public partial class OrdenRepuesto
{
    public int Id { get; set; }

    public int OrdenServicioId { get; set; }

    public int RepuestoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual OrdenServicio OrdenServicio { get; set; } = null!;

    public virtual Repuesto Repuesto { get; set; } = null!;
}
