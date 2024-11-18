IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminEstado] (
        [IdEstado] int NOT NULL IDENTITY,
        [NombreEstado] varchar(55) NOT NULL,
        [Descripcion] varchar(255) NULL,
        CONSTRAINT [PK_AdminEstado] PRIMARY KEY ([IdEstado])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminPermisos] (
        [IdPermiso] int NOT NULL IDENTITY,
        [NombrePermiso] varchar(55) NOT NULL,
        [DescripcionPermiso] varchar(55) NULL,
        CONSTRAINT [PK_AdminPermisos] PRIMARY KEY ([IdPermiso])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminRoles] (
        [IdRole] int NOT NULL IDENTITY,
        [NombreRol] varchar(55) NOT NULL,
        [DescripcionRol] varchar(100) NULL,
        CONSTRAINT [PK_AdminRoles] PRIMARY KEY ([IdRole])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHDepartamento] (
        [IdDepartamentos] int NOT NULL IDENTITY,
        [NombreDepartamento] varchar(55) NOT NULL,
        [DescripcionDepartamento] varchar(100) NULL,
        CONSTRAINT [PK_RHDepartamento] PRIMARY KEY ([IdDepartamentos])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHEstadoCivil] (
        [IdEstadoCivil] int NOT NULL IDENTITY,
        [EstadoCivil] varchar(100) NOT NULL,
        CONSTRAINT [PK_RHEstadoCivil] PRIMARY KEY ([IdEstadoCivil])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHEstadoColaborador] (
        [IdEstadoColaborador] int NOT NULL IDENTITY,
        [EstadosColaborador] varchar(100) NOT NULL,
        [Descripcion] varchar(255) NOT NULL,
        CONSTRAINT [PK_RHEstadoColaborador] PRIMARY KEY ([IdEstadoColaborador])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHEstadoLicencias] (
        [IdEstadoLicencia] int NOT NULL IDENTITY,
        [EstadoLicencia] nvarchar(max) NOT NULL,
        [Descripcion] nvarchar(max) NULL,
        CONSTRAINT [PK_RHEstadoLicencias] PRIMARY KEY ([IdEstadoLicencia])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHTipoLicencias] (
        [IdTipoLicencia] int NOT NULL IDENTITY,
        [TipoLicencia] nvarchar(max) NOT NULL,
        [Descripcion] nvarchar(max) NULL,
        CONSTRAINT [PK_RHTipoLicencias] PRIMARY KEY ([IdTipoLicencia])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminRolesPermisos] (
        [IdRolePermiso] int NOT NULL IDENTITY,
        [IdRole] int NOT NULL,
        [IdPermiso] int NOT NULL,
        CONSTRAINT [PK_AdminRolesPermisos] PRIMARY KEY ([IdRolePermiso]),
        CONSTRAINT [Relationship4] FOREIGN KEY ([IdRole]) REFERENCES [AdminRoles] ([IdRole]),
        CONSTRAINT [Relationship5] FOREIGN KEY ([IdPermiso]) REFERENCES [AdminPermisos] ([IdPermiso])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHColaborador] (
        [IdColaborador] int NOT NULL IDENTITY,
        [IdDepartamento] int NOT NULL,
        [IdEstadoColaborador] int NOT NULL,
        [IdEstadoCivil] int NOT NULL,
        [Codigo] nvarchar(max) NULL,
        [Dpi] nvarchar(max) NOT NULL,
        [Nombres] nvarchar(max) NOT NULL,
        [PrimerApellido] nvarchar(max) NOT NULL,
        [SegundoApellido] nvarchar(max) NULL,
        [ApellidoCasada] nvarchar(max) NULL,
        [MunicipioExtendido] nvarchar(max) NULL,
        [DepartamentoExtendido] nvarchar(max) NULL,
        [LugarNacimiento] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [NoCuentaBancaria] nvarchar(max) NULL,
        [Nacionalidad] nvarchar(max) NULL,
        [NoIGSS] nvarchar(max) NULL,
        [NoNIT] nvarchar(max) NULL,
        [NombreConyuge] nvarchar(max) NULL,
        [Direccion] nvarchar(max) NULL,
        [FechaNacimiento] datetime2 NOT NULL,
        [Email] nvarchar(450) NOT NULL,
        [FechaInicioLabores] datetime2 NULL,
        [Debaja] datetime2 NULL,
        [Foto] nvarchar(max) NULL,
        CONSTRAINT [PK_RHColaborador] PRIMARY KEY ([IdColaborador]),
        CONSTRAINT [FK_RHColaborador_RHDepartamento_IdDepartamento] FOREIGN KEY ([IdDepartamento]) REFERENCES [RHDepartamento] ([IdDepartamentos]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RHColaborador_RHEstadoCivil] FOREIGN KEY ([IdEstadoCivil]) REFERENCES [RHEstadoCivil] ([IdEstadoCivil]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RHColaborador_RHEstadoColaborador] FOREIGN KEY ([IdEstadoColaborador]) REFERENCES [RHEstadoColaborador] ([IdEstadoColaborador]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminUser] (
        [IdUsuario] int NOT NULL IDENTITY,
        [NombreUsuario] varchar(55) NOT NULL,
        [Password] varchar(55) NOT NULL,
        [Email] varchar(100) NOT NULL,
        [FechaCreacion] datetime NOT NULL,
        [IdEstado] int NULL,
        [IdColaborador] int NULL,
        CONSTRAINT [PK_AdminUser] PRIMARY KEY ([IdUsuario]),
        CONSTRAINT [FK_User_User_Gth_Colaborador] FOREIGN KEY ([IdColaborador]) REFERENCES [RHColaborador] ([IdColaborador]),
        CONSTRAINT [Relationship1] FOREIGN KEY ([IdEstado]) REFERENCES [AdminEstado] ([IdEstado])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHHistorialDepartamento] (
        [IdHistorialDepartamento] int NOT NULL IDENTITY,
        [IdColaborador] int NOT NULL,
        [IdDepartamento] int NOT NULL,
        [FechaInicio] datetime2 NOT NULL,
        [FechaFin] datetime2 NULL,
        CONSTRAINT [PK_RHHistorialDepartamento] PRIMARY KEY ([IdHistorialDepartamento]),
        CONSTRAINT [FK_RHHistorialDepartamento_RHColaborador] FOREIGN KEY ([IdColaborador]) REFERENCES [RHColaborador] ([IdColaborador]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RHHistorialDepartamento_RHDepartamento] FOREIGN KEY ([IdDepartamento]) REFERENCES [RHDepartamento] ([IdDepartamentos]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [RHLicencias] (
        [IdLicencias] int NOT NULL IDENTITY,
        [IdColaborador] int NOT NULL,
        [FechaInicio] datetime2 NOT NULL,
        [FechaFin] datetime2 NOT NULL,
        [Observaciones] nvarchar(max) NULL,
        [IdTipoLicencia] int NOT NULL,
        [IdEstadoLicencia] int NOT NULL,
        CONSTRAINT [PK_RHLicencias] PRIMARY KEY ([IdLicencias]),
        CONSTRAINT [FK_RHLicencias_RHColaborador_IdColaborador] FOREIGN KEY ([IdColaborador]) REFERENCES [RHColaborador] ([IdColaborador]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RHLicencias_RHEstadoLicencias_IdEstadoLicencia] FOREIGN KEY ([IdEstadoLicencia]) REFERENCES [RHEstadoLicencias] ([IdEstadoLicencia]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RHLicencias_RHTipoLicencias_IdTipoLicencia] FOREIGN KEY ([IdTipoLicencia]) REFERENCES [RHTipoLicencias] ([IdTipoLicencia]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminBitacoraUsuario] (
        [IdBitacoraUser] int NOT NULL IDENTITY,
        [AccionBitacora] varchar(100) NOT NULL,
        [FechaBitacora] datetime NOT NULL,
        [IdUsuario] int NULL,
        CONSTRAINT [PK_AdminBitacoraUsuario] PRIMARY KEY ([IdBitacoraUser]),
        CONSTRAINT [Relationship3] FOREIGN KEY ([IdUsuario]) REFERENCES [AdminUser] ([IdUsuario])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminResetPassword] (
        [IdToken] int NOT NULL IDENTITY,
        [NombreToken] varchar(255) NOT NULL,
        [FechaCreacionToken] datetime NOT NULL,
        [FechaExpiracionToken] datetime NOT NULL,
        [IdUsuario] int NULL,
        CONSTRAINT [PK_AdminResetPassword] PRIMARY KEY ([IdToken]),
        CONSTRAINT [Relationship2] FOREIGN KEY ([IdUsuario]) REFERENCES [AdminUser] ([IdUsuario])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE TABLE [AdminUserRoles] (
        [IdUserRoles] int NOT NULL IDENTITY,
        [IdUsuario] int NOT NULL,
        [IdRole] int NOT NULL,
        [FechaAsignacion] datetime NOT NULL,
        CONSTRAINT [PK_AdminUserRoles] PRIMARY KEY ([IdUserRoles]),
        CONSTRAINT [Relationship7] FOREIGN KEY ([IdUsuario]) REFERENCES [AdminUser] ([IdUsuario]),
        CONSTRAINT [Relationship8] FOREIGN KEY ([IdRole]) REFERENCES [AdminRoles] ([IdRole])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship3] ON [AdminBitacoraUsuario] ([IdUsuario]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship2] ON [AdminResetPassword] ([IdUsuario]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship4] ON [AdminRolesPermisos] ([IdRole]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship5] ON [AdminRolesPermisos] ([IdPermiso]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AdminUser_IdColaborador] ON [AdminUser] ([IdColaborador]) WHERE [IdColaborador] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship1] ON [AdminUser] ([IdEstado]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship7] ON [AdminUserRoles] ([IdUsuario]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_Relationship8] ON [AdminUserRoles] ([IdRole]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE UNIQUE INDEX [IX_RHColaborador_Email] ON [RHColaborador] ([Email]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHColaborador_IdDepartamento] ON [RHColaborador] ([IdDepartamento]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHColaborador_IdEstadoCivil] ON [RHColaborador] ([IdEstadoCivil]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHColaborador_IdEstadoColaborador] ON [RHColaborador] ([IdEstadoColaborador]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHHistorialDepartamento_IdColaborador] ON [RHHistorialDepartamento] ([IdColaborador]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHHistorialDepartamento_IdDepartamento] ON [RHHistorialDepartamento] ([IdDepartamento]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHLicencias_IdColaborador] ON [RHLicencias] ([IdColaborador]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHLicencias_IdEstadoLicencia] ON [RHLicencias] ([IdEstadoLicencia]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    CREATE INDEX [IX_RHLicencias_IdTipoLicencia] ON [RHLicencias] ([IdTipoLicencia]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241112032119_Init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241112032119_Init', N'8.0.8');
END;
GO

COMMIT;
GO

