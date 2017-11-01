CREATE TABLE [dbo].[CrawlSource]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Name] NVARCHAR(100) NOT NULL, 
    [Url] NVARCHAR(255) NULL, 
    [IsDisabled] BIT NULL, 
    CONSTRAINT [PK_CrawlSource] PRIMARY KEY ([Id]),
)
