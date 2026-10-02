using System;
using System.Collections.Generic;

namespace TALLERMECANICO.CORE.Core.Entities;

public partial class OrdenServicio
{
    public int Id { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string DescripcionProblema { get; set; } = null!;

    public decimal CostoEstimado { get; set; }

    public string Estado { get; set; } = null!;

    public int VehiculoId { get; set; }

    public int TipoServicioId { get; set; }

    public virtual ICollection<OrdenMecanico> OrdenMecanicos { get; set; } = new List<OrdenMecanico>();

    public virtual ICollection<OrdenRepuesto> OrdenRepuestos { get; set; } = new List<OrdenRepuesto>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual TipoServicio TipoServicio { get; set; } = null!;

    public virtual Vehiculo Vehiculo { get; set; } = null!;
}
