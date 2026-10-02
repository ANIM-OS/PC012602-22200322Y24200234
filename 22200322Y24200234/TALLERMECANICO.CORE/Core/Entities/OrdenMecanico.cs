using System;
using System.Collections.Generic;

namespace TALLERMECANICO.CORE.Core.Entities;

public partial class OrdenMecanico
{
    public int Id { get; set; }

    public int OrdenServicioId { get; set; }

    public int MecanicoId { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public string? Observaciones { get; set; }

    public virtual Mecanico Mecanico { get; set; } = null!;

    public virtual OrdenServicio OrdenServicio { get; set; } = null!;
}
