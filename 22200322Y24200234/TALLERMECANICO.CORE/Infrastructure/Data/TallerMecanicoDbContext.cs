using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TALLERMECANICO.CORE.Core.Entities;

namespace TALLERMECANICO.CORE.Infrastructure.Data;

public partial class TallerMecanicoDbContext : DbContext
{
    public TallerMecanicoDbContext(DbContextOptions<TallerMecanicoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Mecanico> Mecanicos { get; set; }

    public virtual DbSet<OrdenMecanico> OrdenMecanicos { get; set; }

    public virtual DbSet<OrdenRepuesto> OrdenRepuestos { get; set; }

    public virtual DbSet<OrdenServicio> OrdenServicios { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Repuesto> Repuestos { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.Property(e => e.Correo).HasMaxLength(254);
            entity.Property(e => e.Materno).HasMaxLength(80);
            entity.Property(e => e.Nombres).HasMaxLength(120);
            entity.Property(e => e.Paterno).HasMaxLength(80);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Mecanico>(entity =>
        {
            entity.ToTable("Mecanico");

            entity.HasIndex(e => e.Documento, "UQ_Mecanico_Documento").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Mecanico_Activo");
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.Materno).HasMaxLength(80);
            entity.Property(e => e.Nombres).HasMaxLength(120);
            entity.Property(e => e.Paterno).HasMaxLength(80);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrdenMecanico>(entity =>
        {
            entity.ToTable("OrdenMecanico");

            entity.HasIndex(e => e.MecanicoId, "IX_OrdenMecanico_MecanicoId");

            entity.HasIndex(e => new { e.OrdenServicioId, e.MecanicoId }, "UQ_OrdenMecanico_Orden_Mecanico").IsUnique();

            entity.Property(e => e.FechaAsignacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_OrdenMecanico_FechaAsignacion");
            entity.Property(e => e.Observaciones).HasMaxLength(500);

            entity.HasOne(d => d.Mecanico).WithMany(p => p.OrdenMecanicos)
                .HasForeignKey(d => d.MecanicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenMecanico_Mecanico");

            entity.HasOne(d => d.OrdenServicio).WithMany(p => p.OrdenMecanicos)
                .HasForeignKey(d => d.OrdenServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenMecanico_OrdenServicio");
        });

        modelBuilder.Entity<OrdenRepuesto>(entity =>
        {
            entity.ToTable("OrdenRepuesto");

            entity.HasIndex(e => e.OrdenServicioId, "IX_OrdenRepuesto_OrdenServicioId");

            entity.HasIndex(e => e.RepuestoId, "IX_OrdenRepuesto_RepuestoId");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("(CONVERT([decimal](20,2),[Cantidad]*[PrecioUnitario]))", true)
                .HasColumnType("decimal(20, 2)");

            entity.HasOne(d => d.OrdenServicio).WithMany(p => p.OrdenRepuestos)
                .HasForeignKey(d => d.OrdenServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenRepuesto_OrdenServicio");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.OrdenRepuestos)
                .HasForeignKey(d => d.RepuestoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenRepuesto_Repuesto");
        });

        modelBuilder.Entity<OrdenServicio>(entity =>
        {
            entity.ToTable("OrdenServicio");

            entity.HasIndex(e => e.TipoServicioId, "IX_OrdenServicio_TipoServicioId");

            entity.HasIndex(e => e.VehiculoId, "IX_OrdenServicio_VehiculoId");

            entity.Property(e => e.CostoEstimado).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DescripcionProblema).HasMaxLength(1000);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Pendiente", "DF_OrdenServicio_Estado");
            entity.Property(e => e.FechaIngreso)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_OrdenServicio_FechaIngreso");

            entity.HasOne(d => d.TipoServicio).WithMany(p => p.OrdenServicios)
                .HasForeignKey(d => d.TipoServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenServicio_TipoServicio");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.OrdenServicios)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenServicio_Vehiculo");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.ToTable("Pago");

            entity.HasIndex(e => e.OrdenServicioId, "IX_Pago_OrdenServicioId");

            entity.Property(e => e.Estado)
                .HasMaxLength(15)
                .HasDefaultValue("Confirmado", "DF_Pago_Estado");
            entity.Property(e => e.FechaPago)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Pago_FechaPago");
            entity.Property(e => e.MetodoPago).HasMaxLength(20);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroOperacion).HasMaxLength(100);

            entity.HasOne(d => d.OrdenServicio).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.OrdenServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_OrdenServicio");
        });

        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.ToTable("Repuesto");

            entity.HasIndex(e => e.Codigo, "UQ_Repuesto_Codigo").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Repuesto_Activo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Marca).HasMaxLength(60);
            entity.Property(e => e.Nombre).HasMaxLength(120);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.ToTable("TipoServicio");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioBase).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculo");

            entity.HasIndex(e => e.ClienteId, "IX_Vehiculo_ClienteId");

            entity.HasIndex(e => e.Placa, "UQ_Vehiculo_Placa").IsUnique();

            entity.Property(e => e.Marca).HasMaxLength(60);
            entity.Property(e => e.Modelo).HasMaxLength(80);
            entity.Property(e => e.Placa)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculo_Cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
