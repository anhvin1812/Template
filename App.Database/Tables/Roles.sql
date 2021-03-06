﻿CREATE TABLE [dbo].[Role]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,

    [Description] NVARCHAR(255) NULL, 
    CONSTRAINT [PK_Role_id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK__Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

