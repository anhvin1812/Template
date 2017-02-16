CREATE TABLE [dbo].[Products_Galleries]
(
	[ProductId] INT NOT NULL , 
    [GalleryId] INT NOT NULL, 
    PRIMARY KEY ([GalleryId], [ProductId]), 
    CONSTRAINT [FK_Products_Categories_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]),
	CONSTRAINT [FK_Products_Categories_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [Gallery]([Id])
)
