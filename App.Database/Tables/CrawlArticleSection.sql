CREATE TABLE [dbo].[CrawlArticleSection]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
	[CrawlSourcePageId] INT NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Selector] NVARCHAR(100) NOT NULL, 
    [TitleSelector] NVARCHAR(100) NOT NULL, 
	[LinkSelector] NVARCHAR(100) NOT NULL, 
    [DescriptionSelector] NVARCHAR(100) NOT NULL, 
    [FeaturedImageSelector] NVARCHAR(100) NOT NULL, 
	[FeaturedImageAttribute] NVARCHAR(100) NULL, 
    [FeatureImageSizeIdentity] NVARCHAR(50) NOT NULL, 
    [LargeFeatureImageSizeIdentity] NVARCHAR(50) NOT NULL,
	[IsRelativeUrl] BIT NOT NULL, 
    [BaseUrl] NVARCHAR(100) NULL,  

    CONSTRAINT [PK_CrawlArticleSection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CrawlArticleSection_CrawlSourcePage] FOREIGN KEY ([CrawlSourcePageId]) REFERENCES [CrawlSourcePage]([Id]),
)
