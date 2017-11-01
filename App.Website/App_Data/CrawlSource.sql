select * from CrawlSource
select * from CrawlSourcePage
select * from CrawlArticleSection
select * from CrawlSourcePageDetail


--Báo Nghệ AN: Pages

Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Trang Chủ', 'http://baonghean.vn', 0, null)

Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Thời sự - Chính trị', 'http://baonghean.vn/thoi-su-chinh-tri', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Xã hội', 'http://baonghean.vn/xa-hoi', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Kinh tế', 'http://baonghean.vn/kinh-te', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Đời sống pháp luật', 'http://baonghean.vn/doi-song-phap-luat', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Quốc phòng', 'http://baonghean.vn/quoc-phong', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Giáo dục - Khoa học', 'http://baonghean.vn/giao-duc-khoa-hoc', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Thể thao', 'http://baonghean.vn/the-thao', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Giải trí', 'http://baonghean.vn/giai-tri', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Quốc tế', 'http://baonghean.vn/quoc-te', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Xe', 'http://baonghean.vn/oto-xe-may', 0, null)
Insert into CrawlSourcePage(CrawlSourceId, Name, Url, IsRelativeUrl, BaseUrl)
	values(2, N'Miền Tây Nghệ An', 'http://baonghean.vn/mien-tay-nghe-an', 0, null)


--Báo Nghệ AN: article section
DECLARE @pageId int
Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Tin mới nhất', '.latest_news ul > li', '.title > a', 'a.avatar', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Theo dòng dự kiện', '.theodongsukien ul > li', '.title > a', 'a.avatar', 'NoDescription', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Tin mới', '.home-cate-list-tinmoi .media', '.media-heading > a', '.media-heading > a', '.media-body', 'a.media-left > img', 'src', 'normal', 'original', 1, 'http://baonghean.vn')
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Thể loại khác', '.home-cate-box-v12 .body > .item', '.title > a', '.title > a', 'NoDescription', 'a > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/thoi-su-chinh-tri'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/xa-hoi'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/kinh-te'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/doi-song-phap-luat'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/quoc-phong'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/giao-duc-khoa-hoc'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/the-thao'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/giai-tri'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/quoc-te'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/oto-xe-may'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

Select @pageId = Id From CrawlSourcePage where Url ='http://baonghean.vn/mien-tay-nghe-an'
Insert into CrawlArticleSection(CrawlSourcePageId, Name, Selector, TitleSelector, LinkSelector, DescriptionSelector, FeaturedImageSelector, FeaturedImageAttribute, FeatureImageSizeIdentity, LargeFeatureImageSizeIdentity, IsRelativeUrl, BaseUrl)
	values(@pageId, N'Trang đầu', 'ul#cate-content > li.item .bgborder', 'h1 > a', 'h1 > a', 'div.lead', 'a.avatar > img', 'src', 'original', 'original', 1, 'http://baonghean.vn')

--Báo Nghệ AN: article detail
DECLARE @sourceId int
Select @sourceId = Id From CrawlSource where Url ='http://baonghean.vn'
Insert into CrawlSourcePageDetail(CrawlSourceId, TitleSelector, DescriptionSelector, ContentSelector, RemoveFromContentSelector, TagSelector, CategorySelector, VideoSelector, VideoSourceSelector, BaseUrl)
	values(@sourceId, '.article h1.title', '#content > p:first-child > strong', '.article #content', 'div > table.rl', '#tags_list > a', '#nav > a', 'video', 'video > source:first-child', 'http://baonghean.vn')

