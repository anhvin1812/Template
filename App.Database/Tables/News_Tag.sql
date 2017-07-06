CREATE TABLE [dbo].[News_Tag]
(
    [NewsId] INT NOT NULL,
    [TagId] INT NOT NULL,

    CONSTRAINT [PK_NewsTag_NewsId_TagId] PRIMARY KEY CLUSTERED ([NewsId] ASC, [TagId] ASC),
    CONSTRAINT [FK_NewsTag_News] FOREIGN KEY ([NewsId]) REFERENCES [dbo].[News] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsTag_Tag] FOREIGN KEY ([TagId]) REFERENCES [dbo].[Tag] ([Id]) ON DELETE CASCADE
);

