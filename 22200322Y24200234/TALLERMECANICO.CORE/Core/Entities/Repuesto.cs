using System;
using System.Collections.Generic;

namespace TALLERMECANICO.CORE.Core.Entities;

public partial class Repuesto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Marca { get; set; }

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<OrdenRepuesto> OrdenRepuestos { get; set; } = new List<OrdenRepuesto>();
}
