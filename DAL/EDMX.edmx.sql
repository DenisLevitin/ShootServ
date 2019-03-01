
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2018 11:43:50
-- Generated from EDMX file: C:\projects\SS\ShootServ\DAL\EDMX.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ShootingCompetitionRequests];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CompetitionType_WeaponTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompetitionType] DROP CONSTRAINT [FK_CompetitionType_WeaponTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Cups_CupTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cups] DROP CONSTRAINT [FK_Cups_CupTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryForCompetitions_EntryStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntryForCompetitions] DROP CONSTRAINT [FK_EntryForCompetitions_EntryStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_CupCompetitionType_CompetitionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CupCompetitionType] DROP CONSTRAINT [FK_CupCompetitionType_CompetitionType];
GO
IF OBJECT_ID(N'[dbo].[FK_CupCompetitionType_Cups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CupCompetitionType] DROP CONSTRAINT [FK_CupCompetitionType_Cups];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryForCompetitions_CupCompetitionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntryForCompetitions] DROP CONSTRAINT [FK_EntryForCompetitions_CupCompetitionType];
GO
IF OBJECT_ID(N'[dbo].[FK_Results_CupCompetitionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_Results_CupCompetitionType];
GO
IF OBJECT_ID(N'[dbo].[FK_EntryForCompetitions_Shooters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EntryForCompetitions] DROP CONSTRAINT [FK_EntryForCompetitions_Shooters];
GO
IF OBJECT_ID(N'[dbo].[FK_Results_Shooters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_Results_Shooters];
GO
IF OBJECT_ID(N'[dbo].[FK_Shooters_ShooterCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shooters] DROP CONSTRAINT [FK_Shooters_ShooterCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Shooters_WeaponTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shooters] DROP CONSTRAINT [FK_Shooters_WeaponTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_Cups_ShootingRanges]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cups] DROP CONSTRAINT [FK_Cups_ShootingRanges];
GO
IF OBJECT_ID(N'[dbo].[FK_ShooterClubs_ShootingRanges]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShooterClubs] DROP CONSTRAINT [FK_ShooterClubs_ShootingRanges];
GO
IF OBJECT_ID(N'[dbo].[FK_Shooters_ShooterClubs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shooters] DROP CONSTRAINT [FK_Shooters_ShooterClubs];
GO
IF OBJECT_ID(N'[dbo].[FK_Cups_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cups] DROP CONSTRAINT [FK_Cups_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Users_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_ShooterClubs_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShooterClubs] DROP CONSTRAINT [FK_ShooterClubs_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Shooters_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shooters] DROP CONSTRAINT [FK_Shooters_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_ShootingRanges_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShootingRanges] DROP CONSTRAINT [FK_ShootingRanges_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_ShootingRanges_Regions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShootingRanges] DROP CONSTRAINT [FK_ShootingRanges_Regions];
GO
IF OBJECT_ID(N'[dbo].[FK_RecoveredPasswords_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecoveredPasswords] DROP CONSTRAINT [FK_RecoveredPasswords_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Regions_Countries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Regions] DROP CONSTRAINT [FK_Regions_Countries];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CupTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CupTypes];
GO
IF OBJECT_ID(N'[dbo].[EntryStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntryStatus];
GO
IF OBJECT_ID(N'[dbo].[Results]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Results];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[ShooterCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShooterCategory];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[WeaponTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeaponTypes];
GO
IF OBJECT_ID(N'[dbo].[CompetitionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompetitionType];
GO
IF OBJECT_ID(N'[dbo].[Cups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cups];
GO
IF OBJECT_ID(N'[dbo].[EntryForCompetitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EntryForCompetitions];
GO
IF OBJECT_ID(N'[dbo].[CupCompetitionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CupCompetitionType];
GO
IF OBJECT_ID(N'[dbo].[Shooters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Shooters];
GO
IF OBJECT_ID(N'[dbo].[ShootingRanges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShootingRanges];
GO
IF OBJECT_ID(N'[dbo].[ShooterClubs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShooterClubs];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Regions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Regions];
GO
IF OBJECT_ID(N'[dbo].[RecoveredPasswords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecoveredPasswords];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CupTypes'
CREATE TABLE [dbo].[CupTypes] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Keychar] varchar(50)  NOT NULL
);
GO

-- Creating table 'EntryStatus'
CREATE TABLE [dbo].[EntryStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Keychar] varchar(50)  NOT NULL
);
GO

-- Creating table 'Results'
CREATE TABLE [dbo].[Results] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdShooter] int  NOT NULL,
    [IdCompetitionTypeCup] int  NOT NULL,
    [ResultInPoints] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [KeyChar] varchar(50)  NOT NULL
);
GO

-- Creating table 'ShooterCategory'
CREATE TABLE [dbo].[ShooterCategory] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Keychar] varchar(50)  NOT NULL,
    [OrderSort] int  NOT NULL,
	[PictureUrl] varchar(100) NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'WeaponTypes'
CREATE TABLE [dbo].[WeaponTypes] (
    [Id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [KeyChar] varchar(50)  NOT NULL,
	[PictureUrl] varchar(100) NOT NULL
);
GO

-- Creating table 'CompetitionType'
CREATE TABLE [dbo].[CompetitionType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [IdWeaponType] int  NOT NULL,
    [SeriesCount] int  NULL
);
GO

-- Creating table 'Cups'
CREATE TABLE [dbo].[Cups] (
    [IdCup] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(250)  NOT NULL,
    [IdCupType] int  NOT NULL,
    [DateStart] datetime  NOT NULL,
    [DateEnd] datetime  NOT NULL,
    [IdShootingRange] int  NOT NULL,
    [Document] varbinary(50)  NULL,
    [IdUser] int  NOT NULL,
    [DateCreate] datetime  NOT NULL
);
GO

-- Creating table 'EntryForCompetitions'
CREATE TABLE [dbo].[EntryForCompetitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdShooter] int  NOT NULL,
    [IdCupCompetitionType] int  NOT NULL,
    [DateCreate] datetime  NOT NULL,
    [IdEntryStatus] int  NOT NULL
);
GO

-- Creating table 'CupCompetitionType'
CREATE TABLE [dbo].[CupCompetitionType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdCup] int  NOT NULL,
    [IdCompetitionType] int  NOT NULL,
    [TimeFirstShift] datetime  NULL
);
GO

-- Creating table 'Shooters'
CREATE TABLE [dbo].[Shooters] (
    [IdShooter] int IDENTITY(1,1) NOT NULL,
    [Family] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [FatherName] varchar(50)  NULL,
    [IdClub] int NULL,
    [Address] varchar(50)  NULL,
    [IdCategory] int  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [IdWeaponType] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [DateCreate] datetime  NOT NULL,
    [Sex] bit  NOT NULL
);
GO

-- Creating table 'ShootingRanges'
CREATE TABLE [dbo].[ShootingRanges] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Address] varchar(50)  NOT NULL,
    [Telefon] varchar(20)  NOT NULL,
    [Info] varchar(max)  NOT NULL,
    [IdRegion] int  NOT NULL,
    [Town] varchar(50)  NOT NULL,
    [IdUser] int  NOT NULL
);
GO

-- Creating table 'ShooterClubs'
CREATE TABLE [dbo].[ShooterClubs] (
    [IdClub] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Address] varchar(250)  NULL,
    [Phone] varchar(20)  NULL,
    [MainCoach] varchar(50)  NULL,
    [IdShootingRange] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [DateCreate] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Family] varchar(50)  NOT NULL,
    [FatherName] varchar(50)  NULL,
    [Login] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [IdRole] int  NOT NULL,
    [DateCreate] datetime  NOT NULL,
    [E_mail] varchar(50)  NOT NULL
);
GO

-- Creating table 'Regions'
CREATE TABLE [dbo].[Regions] (
    [IdRegion] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [FederationAddress] varchar(300)  NOT NULL,
    [FederationTelefon] varchar(25)  NOT NULL,
    [IdCountry] int  NOT NULL
);
GO

-- Creating table 'RecoveredPasswords'
CREATE TABLE [dbo].[RecoveredPasswords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [IdUser] int  NOT NULL,
    [IsRecovered] bit  NOT NULL,
    [ActionDate] datetime  NOT NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int  NOT NULL,
    [CODE] varchar(50)  NULL,
    [CountryName] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CupTypes'
ALTER TABLE [dbo].[CupTypes]
ADD CONSTRAINT [PK_CupTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EntryStatus'
ALTER TABLE [dbo].[EntryStatus]
ADD CONSTRAINT [PK_EntryStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [PK_Results]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ShooterCategory'
ALTER TABLE [dbo].[ShooterCategory]
ADD CONSTRAINT [PK_ShooterCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'WeaponTypes'
ALTER TABLE [dbo].[WeaponTypes]
ADD CONSTRAINT [PK_WeaponTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompetitionType'
ALTER TABLE [dbo].[CompetitionType]
ADD CONSTRAINT [PK_CompetitionType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdCup] in table 'Cups'
ALTER TABLE [dbo].[Cups]
ADD CONSTRAINT [PK_Cups]
    PRIMARY KEY CLUSTERED ([IdCup] ASC);
GO

-- Creating primary key on [Id] in table 'EntryForCompetitions'
ALTER TABLE [dbo].[EntryForCompetitions]
ADD CONSTRAINT [PK_EntryForCompetitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CupCompetitionType'
ALTER TABLE [dbo].[CupCompetitionType]
ADD CONSTRAINT [PK_CupCompetitionType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdShooter] in table 'Shooters'
ALTER TABLE [dbo].[Shooters]
ADD CONSTRAINT [PK_Shooters]
    PRIMARY KEY CLUSTERED ([IdShooter] ASC);
GO

-- Creating primary key on [Id] in table 'ShootingRanges'
ALTER TABLE [dbo].[ShootingRanges]
ADD CONSTRAINT [PK_ShootingRanges]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdClub] in table 'ShooterClubs'
ALTER TABLE [dbo].[ShooterClubs]
ADD CONSTRAINT [PK_ShooterClubs]
    PRIMARY KEY CLUSTERED ([IdClub] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdRegion] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [PK_Regions]
    PRIMARY KEY CLUSTERED ([IdRegion] ASC);
GO

-- Creating primary key on [Id] in table 'RecoveredPasswords'
ALTER TABLE [dbo].[RecoveredPasswords]
ADD CONSTRAINT [PK_RecoveredPasswords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdWeaponType] in table 'CompetitionType'
ALTER TABLE [dbo].[CompetitionType]
ADD CONSTRAINT [FK_CompetitionType_WeaponTypes]
    FOREIGN KEY ([IdWeaponType])
    REFERENCES [dbo].[WeaponTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompetitionType_WeaponTypes'
CREATE INDEX [IX_FK_CompetitionType_WeaponTypes]
ON [dbo].[CompetitionType]
    ([IdWeaponType]);
GO

-- Creating foreign key on [IdCupType] in table 'Cups'
ALTER TABLE [dbo].[Cups]
ADD CONSTRAINT [FK_Cups_CupTypes]
    FOREIGN KEY ([IdCupType])
    REFERENCES [dbo].[CupTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cups_CupTypes'
CREATE INDEX [IX_FK_Cups_CupTypes]
ON [dbo].[Cups]
    ([IdCupType]);
GO

-- Creating foreign key on [IdEntryStatus] in table 'EntryForCompetitions'
ALTER TABLE [dbo].[EntryForCompetitions]
ADD CONSTRAINT [FK_EntryForCompetitions_EntryStatus]
    FOREIGN KEY ([IdEntryStatus])
    REFERENCES [dbo].[EntryStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryForCompetitions_EntryStatus'
CREATE INDEX [IX_FK_EntryForCompetitions_EntryStatus]
ON [dbo].[EntryForCompetitions]
    ([IdEntryStatus]);
GO

-- Creating foreign key on [IdCompetitionType] in table 'CupCompetitionType'
ALTER TABLE [dbo].[CupCompetitionType]
ADD CONSTRAINT [FK_CupCompetitionType_CompetitionType]
    FOREIGN KEY ([IdCompetitionType])
    REFERENCES [dbo].[CompetitionType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CupCompetitionType_CompetitionType'
CREATE INDEX [IX_FK_CupCompetitionType_CompetitionType]
ON [dbo].[CupCompetitionType]
    ([IdCompetitionType]);
GO

-- Creating foreign key on [IdCup] in table 'CupCompetitionType'
ALTER TABLE [dbo].[CupCompetitionType]
ADD CONSTRAINT [FK_CupCompetitionType_Cups]
    FOREIGN KEY ([IdCup])
    REFERENCES [dbo].[Cups]
        ([IdCup])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CupCompetitionType_Cups'
CREATE INDEX [IX_FK_CupCompetitionType_Cups]
ON [dbo].[CupCompetitionType]
    ([IdCup]);
GO

-- Creating foreign key on [IdCupCompetitionType] in table 'EntryForCompetitions'
ALTER TABLE [dbo].[EntryForCompetitions]
ADD CONSTRAINT [FK_EntryForCompetitions_CupCompetitionType]
    FOREIGN KEY ([IdCupCompetitionType])
    REFERENCES [dbo].[CupCompetitionType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryForCompetitions_CupCompetitionType'
CREATE INDEX [IX_FK_EntryForCompetitions_CupCompetitionType]
ON [dbo].[EntryForCompetitions]
    ([IdCupCompetitionType]);
GO

-- Creating foreign key on [IdCompetitionTypeCup] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_Results_CupCompetitionType]
    FOREIGN KEY ([IdCompetitionTypeCup])
    REFERENCES [dbo].[CupCompetitionType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Results_CupCompetitionType'
CREATE INDEX [IX_FK_Results_CupCompetitionType]
ON [dbo].[Results]
    ([IdCompetitionTypeCup]);
GO

-- Creating foreign key on [IdShooter] in table 'EntryForCompetitions'
ALTER TABLE [dbo].[EntryForCompetitions]
ADD CONSTRAINT [FK_EntryForCompetitions_Shooters]
    FOREIGN KEY ([IdShooter])
    REFERENCES [dbo].[Shooters]
        ([IdShooter])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EntryForCompetitions_Shooters'
CREATE INDEX [IX_FK_EntryForCompetitions_Shooters]
ON [dbo].[EntryForCompetitions]
    ([IdShooter]);
GO

-- Creating foreign key on [IdShooter] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_Results_Shooters]
    FOREIGN KEY ([IdShooter])
    REFERENCES [dbo].[Shooters]
        ([IdShooter])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Results_Shooters'
CREATE INDEX [IX_FK_Results_Shooters]
ON [dbo].[Results]
    ([IdShooter]);
GO

-- Creating foreign key on [IdCategory] in table 'Shooters'
ALTER TABLE [dbo].[Shooters]
ADD CONSTRAINT [FK_Shooters_ShooterCategory]
    FOREIGN KEY ([IdCategory])
    REFERENCES [dbo].[ShooterCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shooters_ShooterCategory'
CREATE INDEX [IX_FK_Shooters_ShooterCategory]
ON [dbo].[Shooters]
    ([IdCategory]);
GO

-- Creating foreign key on [IdWeaponType] in table 'Shooters'
ALTER TABLE [dbo].[Shooters]
ADD CONSTRAINT [FK_Shooters_WeaponTypes]
    FOREIGN KEY ([IdWeaponType])
    REFERENCES [dbo].[WeaponTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shooters_WeaponTypes'
CREATE INDEX [IX_FK_Shooters_WeaponTypes]
ON [dbo].[Shooters]
    ([IdWeaponType]);
GO

-- Creating foreign key on [IdShootingRange] in table 'Cups'
ALTER TABLE [dbo].[Cups]
ADD CONSTRAINT [FK_Cups_ShootingRanges]
    FOREIGN KEY ([IdShootingRange])
    REFERENCES [dbo].[ShootingRanges]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cups_ShootingRanges'
CREATE INDEX [IX_FK_Cups_ShootingRanges]
ON [dbo].[Cups]
    ([IdShootingRange]);
GO

-- Creating foreign key on [IdShootingRange] in table 'ShooterClubs'
ALTER TABLE [dbo].[ShooterClubs]
ADD CONSTRAINT [FK_ShooterClubs_ShootingRanges]
    FOREIGN KEY ([IdShootingRange])
    REFERENCES [dbo].[ShootingRanges]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShooterClubs_ShootingRanges'
CREATE INDEX [IX_FK_ShooterClubs_ShootingRanges]
ON [dbo].[ShooterClubs]
    ([IdShootingRange]);
GO

-- Creating foreign key on [IdClub] in table 'Shooters'
ALTER TABLE [dbo].[Shooters]
ADD CONSTRAINT [FK_Shooters_ShooterClubs]
    FOREIGN KEY ([IdClub])
    REFERENCES [dbo].[ShooterClubs]
        ([IdClub])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shooters_ShooterClubs'
CREATE INDEX [IX_FK_Shooters_ShooterClubs]
ON [dbo].[Shooters]
    ([IdClub]);
GO

-- Creating foreign key on [IdUser] in table 'Cups'
ALTER TABLE [dbo].[Cups]
ADD CONSTRAINT [FK_Cups_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cups_Users'
CREATE INDEX [IX_FK_Cups_Users]
ON [dbo].[Cups]
    ([IdUser]);
GO

-- Creating foreign key on [IdRole] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_Users_Roles]
    FOREIGN KEY ([IdRole])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Users_Roles'
CREATE INDEX [IX_FK_Users_Roles]
ON [dbo].[Users]
    ([IdRole]);
GO

-- Creating foreign key on [IdUser] in table 'ShooterClubs'
ALTER TABLE [dbo].[ShooterClubs]
ADD CONSTRAINT [FK_ShooterClubs_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShooterClubs_Users'
CREATE INDEX [IX_FK_ShooterClubs_Users]
ON [dbo].[ShooterClubs]
    ([IdUser]);
GO

-- Creating foreign key on [IdUser] in table 'Shooters'
ALTER TABLE [dbo].[Shooters]
ADD CONSTRAINT [FK_Shooters_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shooters_Users'
CREATE INDEX [IX_FK_Shooters_Users]
ON [dbo].[Shooters]
    ([IdUser]);
GO

-- Creating foreign key on [IdUser] in table 'ShootingRanges'
ALTER TABLE [dbo].[ShootingRanges]
ADD CONSTRAINT [FK_ShootingRanges_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShootingRanges_Users'
CREATE INDEX [IX_FK_ShootingRanges_Users]
ON [dbo].[ShootingRanges]
    ([IdUser]);
GO

-- Creating foreign key on [IdRegion] in table 'ShootingRanges'
ALTER TABLE [dbo].[ShootingRanges]
ADD CONSTRAINT [FK_ShootingRanges_Regions]
    FOREIGN KEY ([IdRegion])
    REFERENCES [dbo].[Regions]
        ([IdRegion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShootingRanges_Regions'
CREATE INDEX [IX_FK_ShootingRanges_Regions]
ON [dbo].[ShootingRanges]
    ([IdRegion]);
GO

-- Creating foreign key on [IdUser] in table 'RecoveredPasswords'
ALTER TABLE [dbo].[RecoveredPasswords]
ADD CONSTRAINT [FK_RecoveredPasswords_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecoveredPasswords_Users'
CREATE INDEX [IX_FK_RecoveredPasswords_Users]
ON [dbo].[RecoveredPasswords]
    ([IdUser]);
GO

-- Creating foreign key on [IdCountry] in table 'Regions'
ALTER TABLE [dbo].[Regions]
ADD CONSTRAINT [FK_Regions_Countries]
    FOREIGN KEY ([IdCountry])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Regions_Countries'
CREATE INDEX [IX_FK_Regions_Countries]
ON [dbo].[Regions]
    ([IdCountry]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------