using System;
using System.Collections.Generic;

namespace TALLERMECANICO.CORE.Core.Entities;

public partial class Mecanico
{
    public int Id { get; set; }

    public string Documento { get; set; } = null!;

    public string Paterno { get; set; } = null!;

    public string? Materno { get; set; }

    public string Nombres { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Especialidad { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<OrdenMecanico> OrdenMecanicos { get; set; } = new List<OrdenMecanico>();
}
