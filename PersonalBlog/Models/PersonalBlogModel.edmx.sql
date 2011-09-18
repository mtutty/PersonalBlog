
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/26/2011 21:03:02
-- Generated from EDMX file: c:\users\michael\documents\visual studio 2010\Projects\PersonalBlog\PersonalBlog\Models\PersonalBlogModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PersonalBlog];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BlogPosts'
CREATE TABLE [dbo].[BlogPosts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [PublishStatus] nvarchar(1)  NOT NULL,
    [RewriteID] nvarchar(50)  NULL,
    [Author] nvarchar(255)  NOT NULL,
    [ContentType] nvarchar(1)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [ViewCount] int  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TagValue] nvarchar(50)  NOT NULL,
    [BlogPost_Id] int  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(255)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CommentText] nvarchar(max)  NOT NULL,
    [BlogPost_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BlogPosts'
ALTER TABLE [dbo].[BlogPosts]
ADD CONSTRAINT [PK_BlogPosts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BlogPost_Id] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [FK_BlogPostTag]
    FOREIGN KEY ([BlogPost_Id])
    REFERENCES [dbo].[BlogPosts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogPostTag'
CREATE INDEX [IX_FK_BlogPostTag]
ON [dbo].[Tags]
    ([BlogPost_Id]);
GO

-- Creating foreign key on [BlogPost_Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_BlogPostComment]
    FOREIGN KEY ([BlogPost_Id])
    REFERENCES [dbo].[BlogPosts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogPostComment'
CREATE INDEX [IX_FK_BlogPostComment]
ON [dbo].[Comments]
    ([BlogPost_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------