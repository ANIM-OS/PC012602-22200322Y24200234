-- Ejecutar una sola vez, despues de 001_crear_base.sql, con sqlcmd -b.
USE TallerMecanicoDB;
GO
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
BEGIN TRANSACTION;

CREATE TABLE dbo.Mecanico (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Mecanico PRIMARY KEY,
    Documento VARCHAR(20) NOT NULL CONSTRAINT UQ_Mecanico_Documento UNIQUE,
    Paterno NVARCHAR(80) NOT NULL,
    Materno NVARCHAR(80) NULL,
    Nombres NVARCHAR(120) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Especialidad NVARCHAR(100) NOT NULL,
    Activo BIT NOT NULL CONSTRAINT DF_Mecanico_Activo DEFAULT (1)
);

CREATE TABLE dbo.OrdenMecanico (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_OrdenMecanico PRIMARY KEY,
    OrdenServicioId INT NOT NULL,
    MecanicoId INT NOT NULL,
    FechaAsignacion DATETIME2(0) NOT NULL CONSTRAINT DF_OrdenMecanico_FechaAsignacion DEFAULT (SYSUTCDATETIME()),
    Observaciones NVARCHAR(500) NULL,
    CONSTRAINT UQ_OrdenMecanico_Orden_Mecanico UNIQUE (OrdenServicioId, MecanicoId),
    CONSTRAINT FK_OrdenMecanico_OrdenServicio FOREIGN KEY (OrdenServicioId) REFERENCES dbo.OrdenServicio(Id),
    CONSTRAINT FK_OrdenMecanico_Mecanico FOREIGN KEY (MecanicoId) REFERENCES dbo.Mecanico(Id)
);

CREATE TABLE dbo.Repuesto (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Repuesto PRIMARY KEY,
    Codigo VARCHAR(30) NOT NULL CONSTRAINT UQ_Repuesto_Codigo UNIQUE,
    Nombre NVARCHAR(120) NOT NULL,
    Marca NVARCHAR(60) NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL CONSTRAINT DF_Repuesto_Stock DEFAULT (0),
    Activo BIT NOT NULL CONSTRAINT DF_Repuesto_Activo DEFAULT (1),
    CONSTRAINT CK_Repuesto_PrecioVenta CHECK (PrecioVenta >= 0),
    CONSTRAINT CK_Repuesto_Stock CHECK (Stock >= 0)
);

CREATE TABLE dbo.OrdenRepuesto (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_OrdenRepuesto PRIMARY KEY,
    OrdenServicioId INT NOT NULL,
    RepuestoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    Subtotal AS (CONVERT(DECIMAL(20,2), Cantidad * PrecioUnitario)) PERSISTED,
    CONSTRAINT CK_OrdenRepuesto_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_OrdenRepuesto_PrecioUnitario CHECK (PrecioUnitario >= 0),
    CONSTRAINT FK_OrdenRepuesto_OrdenServicio FOREIGN KEY (OrdenServicioId) REFERENCES dbo.OrdenServicio(Id),
    CONSTRAINT FK_OrdenRepuesto_Repuesto FOREIGN KEY (RepuestoId) REFERENCES dbo.Repuesto(Id)
);

CREATE TABLE dbo.Pago (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Pago PRIMARY KEY,
    OrdenServicioId INT NOT NULL,
    FechaPago DATETIME2(0) NOT NULL CONSTRAINT DF_Pago_FechaPago DEFAULT (SYSUTCDATETIME()),
    Monto DECIMAL(10,2) NOT NULL,
    MetodoPago NVARCHAR(20) NOT NULL,
    NumeroOperacion NVARCHAR(100) NULL,
    Estado NVARCHAR(15) NOT NULL CONSTRAINT DF_Pago_Estado DEFAULT (N'Confirmado'),
    CONSTRAINT CK_Pago_Monto CHECK (Monto > 0),
    CONSTRAINT CK_Pago_MetodoPago CHECK (MetodoPago IN (N'Efectivo', N'Tarjeta', N'Transferencia', N'Yape', N'Plin')),
    CONSTRAINT CK_Pago_Estado CHECK (Estado IN (N'Confirmado', N'Anulado')),
    CONSTRAINT FK_Pago_OrdenServicio FOREIGN KEY (OrdenServicioId) REFERENCES dbo.OrdenServicio(Id)
);

CREATE INDEX IX_OrdenMecanico_MecanicoId ON dbo.OrdenMecanico(MecanicoId);
CREATE INDEX IX_OrdenRepuesto_OrdenServicioId ON dbo.OrdenRepuesto(OrdenServicioId);
CREATE INDEX IX_OrdenRepuesto_RepuestoId ON dbo.OrdenRepuesto(RepuestoId);
CREATE INDEX IX_Pago_OrdenServicioId ON dbo.Pago(OrdenServicioId);

COMMIT TRANSACTION;
GO
