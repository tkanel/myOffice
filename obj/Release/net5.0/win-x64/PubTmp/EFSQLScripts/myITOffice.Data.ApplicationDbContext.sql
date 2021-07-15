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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200625114053_AddMyLoginModel')
BEGIN
    CREATE TABLE [LoginDetails] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(250) NOT NULL,
        [UserName] nvarchar(50) NOT NULL,
        [Password] nvarchar(50) NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_LoginDetails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200625114053_AddMyLoginModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200625114053_AddMyLoginModel', N'5.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720075843_AddProject')
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NOT NULL,
        [Hardware] nvarchar(max) NULL,
        [Software] nvarchar(max) NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200720075843_AddProject')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200720075843_AddProject', N'5.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722084046_AddRackPort')
BEGIN
    CREATE TABLE [RackPorts] (
        [Id] int NOT NULL IDENTITY,
        [Rack] int NOT NULL,
        [Port] int NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_RackPorts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722084046_AddRackPort')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200722084046_AddRackPort', N'5.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722090610_UpdateRackPortAndAddRacks')
BEGIN
    ALTER TABLE [RackPorts] ADD [RackId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722090610_UpdateRackPortAndAddRacks')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200722090610_UpdateRackPortAndAddRacks', N'5.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722090737_AddRacks')
BEGIN
    CREATE TABLE [Racks] (
        [Id] int NOT NULL IDENTITY,
        [Brand] nvarchar(max) NULL,
        [AssetNr] int NOT NULL,
        [PortsNr] int NOT NULL,
        [Notes] nvarchar(max) NULL,
        CONSTRAINT [PK_Racks] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722090737_AddRacks')
BEGIN
    CREATE INDEX [IX_RackPorts_RackId] ON [RackPorts] ([RackId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722090737_AddRacks')
BEGIN
    ALTER TABLE [RackPorts] ADD CONSTRAINT [FK_RackPorts_Racks_RackId] FOREIGN KEY ([RackId]) REFERENCES [Racks] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200722090737_AddRacks')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200722090737_AddRacks', N'5.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201126135832_AddMySecretPasswordsTable')
BEGIN
    CREATE TABLE [MySecretPasswords] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [URL] nvarchar(max) NULL,
        [Username] nvarchar(max) NOT NULL,
        [Passphrase] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MySecretPasswords] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201126135832_AddMySecretPasswordsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201126135832_AddMySecretPasswordsTable', N'5.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202084145_ChangeLengthtoSecretPass')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MySecretPasswords]') AND [c].[name] = N'Username');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [MySecretPasswords] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [MySecretPasswords] ALTER COLUMN [Username] nvarchar(400) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202084145_ChangeLengthtoSecretPass')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MySecretPasswords]') AND [c].[name] = N'Passphrase');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [MySecretPasswords] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [MySecretPasswords] ALTER COLUMN [Passphrase] nvarchar(400) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202084145_ChangeLengthtoSecretPass')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MySecretPasswords]') AND [c].[name] = N'Name');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [MySecretPasswords] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [MySecretPasswords] ALTER COLUMN [Name] nvarchar(400) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201202084145_ChangeLengthtoSecretPass')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201202084145_ChangeLengthtoSecretPass', N'5.0.7');
END;
GO

COMMIT;
GO

