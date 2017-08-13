CREATE TABLE [dbo].[News]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Title] NVARCHAR(255) NOT NULL, 
    [GalleryId] INT NOT NULL, 
	[Description] NVARCHAR(500) NULL, 
    [Content] NTEXT NULL, 
	[IsHot] BIT NULL, 
    [IsFeatured] BIT NULL,
	[StatusId] INT NOT NULL,
	[MediaTypeId] INT NOT NULL,    
	[Views] INT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [DeletedDate] DATETIME NULL, 
    [UpdatedById] INT NOT NULL, 	  
    
    CONSTRAINT [FK_News_Gallery] FOREIGN KEY ([GalleryId]) REFERENCES [Gallery]([Id]), 
    CONSTRAINT [FK_News_User] FOREIGN KEY ([UpdatedById]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_News_Status] FOREIGN KEY ([StatusId]) REFERENCES [NewsStatus]([Id])
)
