CREATE TABLE [dbo].[News_NewsCategory]
(
    [NewsId] INT NOT NULL,
    [NewsCategoryId] INT NOT NULL,

    CONSTRAINT [PK_NewsNewsCategory_NewsId_NewsCategoryId] PRIMARY KEY CLUSTERED ([NewsId] ASC, [NewsCategoryId] ASC),
    CONSTRAINT [FK_NewsNewsCategory_News] FOREIGN KEY ([NewsId]) REFERENCES [dbo].[News] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsNewsCategory_NewsCategory] FOREIGN KEY ([NewsCategoryId]) REFERENCES [dbo].[NewsCategory] ([Id]) ON DELETE CASCADE
);

