CREATE TABLE [dbo].[HomepageLayout]
(
	[Id] INT NOT NULL IDENTITY, 
    [CategoryId] INT NULL, 
    [MediaTypeId] INT NULL, 
    [LayoutTypeId] INT NULL, 
    [SortOrder] INT NULL, 

    CONSTRAINT [PK_HomepageLayout] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_HomepageLayout_NewsCategory] FOREIGN KEY ([CategoryId]) REFERENCES [NewsCategory]([Id])
)
