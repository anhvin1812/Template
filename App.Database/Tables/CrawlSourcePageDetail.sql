CREATE TABLE [dbo].[CrawlSourcePageDetail]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
	[CrawlSourceId] INT NOT NULL, 
    [TitleSelector] NVARCHAR(100) NOT NULL, 
    [DescriptionSelector] NVARCHAR(100) NOT NULL, 
    [ContentSelector] NVARCHAR(100) NOT NULL,
	[RemoveFromContentSelector] NVARCHAR(100) NULL,
	[DateSelector] NVARCHAR(100) NULL, 
    [DateFormat] NVARCHAR(100) NULL, 
    [EditorSelector] NVARCHAR(100) NULL, 
    [TagSelector] NVARCHAR(100) NULL, 
    [CategorySelector] NVARCHAR(100) NULL, 
    [VideoSelector] NVARCHAR(100) NULL, 
    [VideoSourceSelector] NVARCHAR(100) NULL,
	[BaseUrl] NVARCHAR(100) NULL, 

    CONSTRAINT [PK_CrawlSourcePageDetail] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_CrawlSourcePageDetail_CrawlSource] FOREIGN KEY ([CrawlSourceId]) REFERENCES [CrawlSource]([Id]),
)
