/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/* Source*/
IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSource WHERE Url = N'https://vnexpress.net')
	Insert into dbo.CrawlSource(Name, Url) values(N'VnExpress', N'https://vnexpress.net')

IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSource WHERE Url = N'http://www.baonghean.vn')
	Insert into dbo.CrawlSource(Name, Url) values(N'Báo Nghệ An', N'http://www.baonghean.vn')

IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSource WHERE Url = N'http://truyenhinhnghean.vn')
	Insert into dbo.CrawlSource(Name, Url) values(N'Truyền Hình Nghệ An', N'http://truyenhinhnghean.vn')
GO

/* Pages */

DECLARE @SourceId INT

-- VnExpress
SELECT @SourceId = Id FROM dbo.CrawlSource WHERE Url = N'https://vnexpress.net'

IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net')
	Insert into dbo.CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl) values(@SourceId, N'Trang Chủ', N'https://vnexpress.net', 0, null)

IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net/tin-tuc/thoi-su')
	Insert into dbo.CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl) values(@SourceId, N'Thời Sự', N'https://vnexpress.net/tin-tuc/thoi-su', 0, null)

IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net/tin-tuc/the-gioi')
	Insert into dbo.CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl) values(@SourceId, N'Thế Giới', N'https://vnexpress.net/tin-tuc/the-gioi', 0, null)

-- VnExpress section
DECLARE @PageId INT

IF EXISTS(SELECT 1 FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net')
BEGIN
	SELECT @PageId = Id FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net'
	Insert into dbo.CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl) 
		VALUES(1, N'Nổi bật', 'section.featured > article', '.title_news > a', '.title_news > a', '.description', '.thumb_big > a > img', 'data-original', '500x300', '500x300', 0, null)
END

IF EXISTS(SELECT 1 FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net')
BEGIN
	SELECT @PageId = Id FROM dbo.CrawlSourcePage WHERE Url = N'https://vnexpress.net/tin-tuc/thoi-su'
	Insert into dbo.CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl) 
		VALUES(1, N'Nổi bật nhỏ', 'section.sidebar_home_1 > article', '.title_news > a', '.title_news > a', '.description', '.thumb_art > a > img', 'data-original', '140x84', '500x300', 0, null)
END

-- VnExpress article detail
SELECT @SourceId = Id FROM dbo.CrawlSource WHERE Url = N'https://vnexpress.net'
IF NOT EXISTS(SELECT 1 FROM dbo.CrawlSourcePageDetail WHERE Id = @SourceId)
	Insert into CrawlSourcePageDetail(CrawlSourceId, TitleSelector, DescriptionSelector, ContentSelector, CategorySelector, TagSelector, VideoSelector, VideoSourceSelector) 
		VALUES(1, 'h1.title_news_detail, h1.title_detail_video', 'h2.description, #info_lead .lead_detail_video', 'article.content_detail, .fck_detail, #videoContainter', '.cat_header > ul.breadcrumb > li > h4 > a', '.block_tag > h5 > a', '.box_embed_video_parent, video#media-video', '.box_embed_video_parent video, video#media-video')
GO