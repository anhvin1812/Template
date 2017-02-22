CREATE TABLE [dbo].[NewsCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [ParentId] INT NULL, 
    [IsDisabled] BIT NULL, 
    CONSTRAINT [FK_NewsCategory_NewsCategory] FOREIGN KEY ([ParentId]) REFERENCES [NewsCategory]([Id])
)
