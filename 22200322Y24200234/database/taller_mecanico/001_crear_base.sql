-- Ejecutar con sqlcmd -b. No elimina ni reemplaza bases existentes.
USE master;
GO
IF DB_ID(N'TallerMecanicoDB') IS NOT NULL
    THROW 50001, 'TallerMecanicoDB ya existe. No se modifico la base existente.', 1;
GO
CREATE DATABASE TallerMecanicoDB;
GO
USE TallerMecanicoDB;
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;

CREATE TABLE dbo.TipoServicio (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_TipoServicio PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    PrecioBase DECIMAL(10,2) NOT NULL,
    CONSTRAINT CK_TipoServicio_PrecioBase CHECK (PrecioBase >= 0)
);

CREATE TABLE dbo.Cliente (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Cliente PRIMARY KEY,
    Paterno NVARCHAR(80) NOT NULL,
    Materno NVARCHAR(80) NULL,
    Nombres NVARCHAR(120) NOT NULL,
    Correo NVARCHAR(254) NOT NULL,
    Telefono VARCHAR(20) NOT NULL
);

CREATE TABLE dbo.Vehiculo (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Vehiculo PRIMARY KEY,
    Placa VARCHAR(15) NOT NULL CONSTRAINT UQ_Vehiculo_Placa UNIQUE,
    Marca NVARCHAR(60) NOT NULL,
    Modelo NVARCHAR(80) NOT NULL,
    Anio SMALLINT NOT NULL,
    ClienteId INT NOT NULL,
    CONSTRAINT CK_Vehiculo_Anio CHECK (Anio BETWEEN 1886 AND 9999),
    CONSTRAINT FK_Vehiculo_Cliente FOREIGN KEY (ClienteId) REFERENCES dbo.Cliente(Id)
);

CREATE TABLE dbo.OrdenServicio (
    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_OrdenServicio PRIMARY KEY,
    FechaIngreso DATETIME2(0) NOT NULL CONSTRAINT DF_OrdenServicio_FechaIngreso DEFAULT (SYSUTCDATETIME()),
    DescripcionProblema NVARCHAR(1000) NOT NULL,
    CostoEstimado DECIMAL(10,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL CONSTRAINT DF_OrdenServicio_Estado DEFAULT (N'Pendiente'),
    VehiculoId INT NOT NULL,
    TipoServicioId INT NOT NULL,
    CONSTRAINT CK_OrdenServicio_CostoEstimado CHECK (CostoEstimado >= 0),
    CONSTRAINT CK_OrdenServicio_Estado CHECK (Estado IN (N'Pendiente', N'EnProceso', N'Finalizada', N'Cancelada')),
    CONSTRAINT FK_OrdenServicio_Vehiculo FOREIGN KEY (VehiculoId) REFERENCES dbo.Vehiculo(Id),
    CONSTRAINT FK_OrdenServicio_TipoServicio FOREIGN KEY (TipoServicioId) REFERENCES dbo.TipoServicio(Id)
);

CREATE INDEX IX_Vehiculo_ClienteId ON dbo.Vehiculo(ClienteId);
CREATE INDEX IX_OrdenServicio_VehiculoId ON dbo.OrdenServicio(VehiculoId);
CREATE INDEX IX_OrdenServicio_TipoServicioId ON dbo.OrdenServicio(TipoServicioId);

COMMIT TRANSACTION;
GO
