CREATE TABLE [dbo].[CrawlSourcePage]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
	[CrawlSourceId] INT NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Url] NVARCHAR(500) NOT NULL, 
    [IsRelativeUrl] BIT NOT NULL, 
    [BaseUrl] NVARCHAR(100) NULL,
	 
    CONSTRAINT [PK_CrawlSourcePage] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_CrawlSourcePage_CrawlSource] FOREIGN KEY ([CrawlSourceId]) REFERENCES [CrawlSource]([Id]),
)
