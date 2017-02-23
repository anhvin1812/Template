CREATE TABLE [dbo].[News]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Title] NVARCHAR(255) NOT NULL, 
    [GalleryId] INT NOT NULL, 
	[Description] NVARCHAR(255) NULL, 
    [Content] TEXT NULL, 
    [CategoryId] INT NOT NULL, 
	[Views] INT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [DeletedDate] DATETIME NULL, 
    [UpdatedById] INT NOT NULL, 

    CONSTRAINT [FK_News_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [Gallery]([Id]), 
    CONSTRAINT [FK_News_NewsCategory] FOREIGN KEY ([CategoryId]) REFERENCES [NewsCategory]([Id]),
    CONSTRAINT [FK_News_User] FOREIGN KEY ([UpdatedById]) REFERENCES [User]([Id])
)
