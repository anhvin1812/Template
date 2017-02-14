CREATE TABLE [dbo].[ProductCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [ParentId] INT NULL, 
    CONSTRAINT [FK_ProductCategory_ProductCategory] FOREIGN KEY ([ParentId]) REFERENCES [ProductCategory]([Id])
)
