
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/09/2016 11:20:38
-- Generated from EDMX file: E:\OneDrive\sul\xtcx\xtcx\xtcx\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [xtcx];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MemberOrganization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Organizations] DROP CONSTRAINT [FK_MemberOrganization];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizationCase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cases] DROP CONSTRAINT [FK_OrganizationCase];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizationPolicy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Policies] DROP CONSTRAINT [FK_OrganizationPolicy];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizationPerformance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Performances] DROP CONSTRAINT [FK_OrganizationPerformance];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Members]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Members];
GO
IF OBJECT_ID(N'[dbo].[Organizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organizations];
GO
IF OBJECT_ID(N'[dbo].[Cases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cases];
GO
IF OBJECT_ID(N'[dbo].[Policies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Policies];
GO
IF OBJECT_ID(N'[dbo].[Performances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Performances];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Members'
CREATE TABLE [dbo].[Members] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Phone] varchar(50)  NOT NULL,
    [Address] varchar(50)  NOT NULL,
    [MemberType] varchar(50)  NOT NULL,
    [RegTime] datetime  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [WorkingPlace] nvarchar(50)  NULL,
    [Type] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Url] varchar(50)  NULL,
    [Description] varchar(max)  NULL,
    [ScienceDesc] varchar(max)  NULL,
    [IndustryDesc] varchar(max)  NULL,
    [DomainDesc] varchar(max)  NULL,
    [CultureDesc] varchar(max)  NULL,
    [Views] int  NULL,
    [MemberId] int  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [State] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Cases'
CREATE TABLE [dbo].[Cases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [FilePath] nvarchar(50)  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [OrganizationId] int  NOT NULL
);
GO

-- Creating table 'Policies'
CREATE TABLE [dbo].[Policies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [FilePath] nvarchar(50)  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [OrganizationId] int  NOT NULL
);
GO

-- Creating table 'Performances'
CREATE TABLE [dbo].[Performances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [FilePath] nvarchar(50)  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [OrganizationId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [PK_Members]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cases'
ALTER TABLE [dbo].[Cases]
ADD CONSTRAINT [PK_Cases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Policies'
ALTER TABLE [dbo].[Policies]
ADD CONSTRAINT [PK_Policies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Performances'
ALTER TABLE [dbo].[Performances]
ADD CONSTRAINT [PK_Performances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MemberId] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [FK_MemberOrganization]
    FOREIGN KEY ([MemberId])
    REFERENCES [dbo].[Members]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MemberOrganization'
CREATE INDEX [IX_FK_MemberOrganization]
ON [dbo].[Organizations]
    ([MemberId]);
GO

-- Creating foreign key on [OrganizationId] in table 'Cases'
ALTER TABLE [dbo].[Cases]
ADD CONSTRAINT [FK_OrganizationCase]
    FOREIGN KEY ([OrganizationId])
    REFERENCES [dbo].[Organizations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizationCase'
CREATE INDEX [IX_FK_OrganizationCase]
ON [dbo].[Cases]
    ([OrganizationId]);
GO

-- Creating foreign key on [OrganizationId] in table 'Policies'
ALTER TABLE [dbo].[Policies]
ADD CONSTRAINT [FK_OrganizationPolicy]
    FOREIGN KEY ([OrganizationId])
    REFERENCES [dbo].[Organizations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizationPolicy'
CREATE INDEX [IX_FK_OrganizationPolicy]
ON [dbo].[Policies]
    ([OrganizationId]);
GO

-- Creating foreign key on [OrganizationId] in table 'Performances'
ALTER TABLE [dbo].[Performances]
ADD CONSTRAINT [FK_OrganizationPerformance]
    FOREIGN KEY ([OrganizationId])
    REFERENCES [dbo].[Organizations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizationPerformance'
CREATE INDEX [IX_FK_OrganizationPerformance]
ON [dbo].[Performances]
    ([OrganizationId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------