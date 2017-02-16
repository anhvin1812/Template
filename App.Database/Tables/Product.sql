CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(255) NOT NULL, 
    [GalleryId] INT NULL, 
    [Price] DECIMAL(12, 2) NOT NULL, 
    [OldPrice] DECIMAL(12, 2) NULL, 
    [StatusId] INT NOT NULL, 
    [Specifications] TEXT NULL, 
    [CategoryId] INT NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    CONSTRAINT [FK_Product_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [Gallery]([Id]), 
    CONSTRAINT [FK_Product_ProductStatus] FOREIGN KEY ([StatusId]) REFERENCES [ProductStatus]([Id]), 
    CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY ([CategoryId]) REFERENCES [ProductCategory]([Id])
)
