using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Transactions;
using App.Core.Exceptions;
using App.Core.News;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities;
using App.Entities.NewsManagement;
using App.Infrastructure.File;
using App.Repositories.NewsManagement;
using App.Services.Common;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;
using App.Services.Gallery;
using App.Services.IdentityManagement;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace App.Services.NewsManagement
{
    public class CrawlService : ServiceBase, ICrawlService
    {
        #region Contructor
        private ISecurityService SecurityService { get; set; }
        private IGalleryService GalleryService { get; set; }
        private ICrawlRepository CrawlRepository { get; set; }
        private INewsRepository NewsRepository { get; set; }
        private INewsCategoryRepository NewsCategoryRepository { get; set; }
        private ITagRepository TagRepository { get; set; }

        public CrawlService(IUnitOfWork unitOfWork, ISecurityService securityService, IGalleryService galleryService, ICrawlRepository crawlRepository, INewsRepository newsRepository,
                INewsCategoryRepository newsCategoryRepository, ITagRepository tagRepository)
            : base(unitOfWork, new IRepository[] { crawlRepository, newsRepository, newsCategoryRepository, tagRepository }, 
                  new IService[] { securityService })
        { 
            SecurityService = securityService;
            GalleryService = galleryService;

            CrawlRepository = crawlRepository;
            NewsRepository = newsRepository;
            NewsCategoryRepository = newsCategoryRepository;
            TagRepository = tagRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<CrawlSummary> Scan(CrawlFilter filter)
        {
            ValidateCrawlFilter(filter);

            var results = new List<CrawlSummary>();

            var sourcePages = CrawlRepository.GetCrawlSourcePageByIds(filter.CrawlSourcePageIds);

            foreach (var page in sourcePages)
            {
                var htmlWeb = new HtmlWeb()
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
                };

                //Load trang web, nạp html vào document
                var pageUrl = page.IsRelativeUrl ? $"{page.BaseUrl}/{page.Url}" : page.Url;
                var document = htmlWeb.Load(pageUrl);

                var articleSections = CrawlRepository.GetCrawlArticleSectionBySourcePageId(page.Id);
                foreach (var section in articleSections)
                {
                    var articles = document.DocumentNode.QuerySelectorAll(section.Selector);
                    foreach (var article in articles)
                    {
                        var title = article.QuerySelector(section.TitleSelector)?.InnerText;
                        var description = article.QuerySelector(section.DescriptionSelector)?.InnerText;

                        var link = article.QuerySelector(section.LinkSelector)?.Attributes["href"]?.Value;
                        link = section.IsRelativeUrl ? $"{section.BaseUrl}/{link}" : link;
                        
                        var image = article.QuerySelector(section.FeaturedImageSelector);
                        var imageSrc = image.Attributes[section.FeaturedImageAttribute]?.Value ?? image.Attributes["src"].Value;
                        
                       // if (!isUri) // if using lazy loading get the src from data attribute
                          //  imageSrc = image.Attributes[section.FeaturedImageAttribute].Value;

                        imageSrc = section.IsRelativeUrl ? $"{section.BaseUrl}/{imageSrc}" : imageSrc;
                        imageSrc = imageSrc.Replace(section.FeatureImageSizeIdentity, section.LargeFeatureImageSizeIdentity);

                        results.Add(new CrawlSummary
                        {
                            CrawlSourceId = filter.CrawlSourceId,
                            Title = title,
                            Link = link,
                            Desciption = description,
                            FeaturedImage = imageSrc
                        });
                    }
                }
            }

            // filter by keyword
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                return results.Where(x => x.Title.IndexOf(filter.Keyword, StringComparison.OrdinalIgnoreCase) >=0);
            }

            return results;
        }

        public PageArticleDetail GetArticleDetail(int sourceId, string linkArticleDetail)
        {
            var articleDetail = CrawlRepository.GetCrawlSourcePageDetailBySourceId(sourceId);

            if(articleDetail == null)
                throw new DataNotFoundException();

            var htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };

            //Load trang web, nạp html vào document
            var document = htmlWeb.Load(linkArticleDetail);

            var detail = new PageArticleDetail
            {
                Link = linkArticleDetail
            };

            var title = document.DocumentNode.QuerySelector(articleDetail.TitleSelector)?.InnerText;
            var description = document.DocumentNode.QuerySelector(articleDetail.DescriptionSelector)?.InnerText;
            var contentNode = document.DocumentNode.QuerySelector(articleDetail.ContentSelector);
            

            // if cannot get title & content 
            if (string.IsNullOrWhiteSpace(title) || contentNode == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.CanNotGetArticleContent, Property = "Content"}
                };
                throw new ValidationError(violations);
            }
            
            // remove redundant node
            RemoveRedundantContent(contentNode, articleDetail);

            // replace relative images with absolute url
            ReplaceRelativeImage(contentNode, articleDetail);

            // replace video section with video tag
            ReplaceVideos(contentNode, articleDetail);
            var content = contentNode.InnerHtml;

            var tags = document.DocumentNode.QuerySelectorAll(articleDetail.TagSelector);
            foreach (var tag in tags)
            {
                detail.Tags.Add(tag.InnerText);
            }

            var categories = document.DocumentNode.QuerySelectorAll(articleDetail.CategorySelector);
            foreach (var category in categories)
            {
                detail.Categories.Add(category.InnerText);
            }

            detail.Title = title;
            detail.Description = description;
            detail.Content = content;

            return detail;
        }

        public void Save(CrawlEntry entry)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.NEWS, ApplicationPermissions.Create))
                throw new PermissionException();

            // Validate data
            ValidateEntryData(entry);

            var isExisting = NewsRepository.IsExist(entry.Title);
            if (isExisting)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.NewsIsExisting, Property = "Title"}
                };
                throw new ValidationError(violations);
            }

            // status
            var status = NewsRepository.GetStatusById(entry.StatusId);
            if (status == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.NewsStatusIsNotExisted, Property = "Status"}
                };
                throw new ValidationError(violations);
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                var entity = new News
                {
                    Title = entry.Title,
                    Description = entry.Description,
                    Content = entry.Content,
                    IsHot = entry.IsHot,
                    IsFeatured = entry.IsFeatured,
                    StatusId = entry.StatusId,
                    MediaTypeId = (int)MediaType.Standard,
                    CreatedDate = entry.Date ?? DateTime.Now,
                    UpdatedById = userId,

                    Categories = new List<NewsCategory>(),
                    Tags = new List<Tag>()
                };

                // categories
                if (entry.CategoryIds != null && entry.CategoryIds.Any())
                {
                    var categories = NewsCategoryRepository.GetByIds(entry.CategoryIds).ToList();
                    entity.Categories = categories;
                }

                // tags
                if (entry.TagIds != null && entry.TagIds.Any())
                {
                    var tags = TagRepository.GetByIds(entry.TagIds).ToList();
                    entity.Tags = tags;
                }

                // upload image
                Image image = DownloadImage(entry.FeaturedImage);
                if (image != null)
                {
                    string thumbnailName, imageName;

                    GalleryHelper.UploadNewsImage(image, out thumbnailName, out imageName);

                    entity.Image = new Entities.FileManagement.Gallery
                    {
                        Image = imageName,
                        Thumbnail = thumbnailName,
                        State = ObjectState.Added
                    };
                }

                NewsRepository.Insert(entity);
                Save();

                transactionScope.Complete();
            }
        }

        #region Pages

        public IEnumerable<PageSummary> GetPagesBySourceId(int sourceId)
        {
            var results = CrawlRepository.GetCrawlSourcePageBySourceId(sourceId);
            return results.Select(x => new PageSummary
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        #endregion

        #region Dropdown list

        public SelectListOptions GetCrawlSourceOptionsForDropdownList()
        {
            int? recordCount = null;
            var results = CrawlRepository.GetCrawlSources(false, null, null, ref recordCount);

            return new SelectListOptions
            {
                Items = results.Select(x => new OptionItem
                {
                    Value = x.Id,
                    Text = x.Name
                })
            };
        }

        public SelectListOptions GetCrawlSourcePageOptionsForDropdownList(int crawlSourceId)
        {
            var results = CrawlRepository.GetCrawlSourcePageBySourceId(crawlSourceId);

            return new SelectListOptions
            {
                Items = results.Select(x => new OptionItem
                {
                    Value = x.Id,
                    Text = x.Name
                })
            };
        }
        #endregion

        #endregion


        #region Private Methods
        private void ReplaceVideos(HtmlNode contentNode, CrawlSourcePageDetail articleDetail)
        {
            if (string.IsNullOrWhiteSpace(articleDetail.VideoSelector) || string.IsNullOrWhiteSpace(articleDetail.VideoSelector))
                return;
            var videos = contentNode.QuerySelectorAll(articleDetail.VideoSelector);
            var videoSources = contentNode.QuerySelectorAll(articleDetail.VideoSourceSelector);

            if(videos ==null || !videos.Any() || videoSources == null || !videoSources.Any())
                return;

            for (var i = 0; i < videos.Count(); i++)
            {
                var videoSource = videoSources.ElementAtOrDefault(i);
                var src = videoSource?.Attributes["src"]?.Value;
                var type = videoSource?.Attributes["type"]?.Value;

                var newVideo = HtmlNode.CreateNode("<video width=\"100%\" height=\"auto\" controls>" +
                                                   $"<source src=\"{src}\" type=\"{type}\">" +
                                                    "Your browser does not support the video tag." +
                                                    "</video>");

                var parent = videos.ElementAtOrDefault(i).ParentNode;
                parent.ReplaceChild(newVideo, videos.ElementAtOrDefault(i));
            }
        }

        private void RemoveRedundantContent(HtmlNode contentNode, CrawlSourcePageDetail articleDetail)
        {
            if (string.IsNullOrWhiteSpace(articleDetail.RemoveFromContentSelector))
                return;

            var redundantNodes = contentNode.QuerySelectorAll(articleDetail.RemoveFromContentSelector);
            if (redundantNodes == null || !redundantNodes.Any())
                return;

            for (var i = 0; i < redundantNodes.Count(); i++)
            {
                redundantNodes.ElementAtOrDefault(i).Remove(); 
            }
        }

        private void ReplaceRelativeImage(HtmlNode contentNode, CrawlSourcePageDetail articleDetail)
        {
            var images = contentNode.QuerySelectorAll("img");

            foreach (var img in images)
            {
                var src = img.Attributes["src"]?.Value;

                Uri result;
                var isAbsoluteUrl = Uri.TryCreate(src, UriKind.Absolute, out result);
                if (!isAbsoluteUrl && !string.IsNullOrWhiteSpace(src))
                    img.Attributes["src"].Value = $"{articleDetail.BaseUrl}/{src}";
            }
        }

        private void ValidateEntryData(CrawlEntry entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });
                throw new ValidationError(violations);
            }

            if (string.IsNullOrWhiteSpace(entry.Title))
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidTitle, Property = "Title" });
            }

            if (entry.FeaturedImage == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.EmptyImage, Property = "Image" });
            }

            if (entry.CategoryIds == null || !entry.CategoryIds.Any())
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.NewsCategoryIsEmpty, Property = "CategoryIds" });
            }

            if (violations.Any())
                throw new ValidationError(violations);
        }

        private void ValidateCrawlFilter(CrawlFilter entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });
                throw new ValidationError(violations);
            }

            if (violations.Any())
                throw new ValidationError(violations);
        }

        private Image DownloadImage(string imageUrl)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    Stream stream = webClient.OpenRead(imageUrl);

                    Bitmap image = new Bitmap(stream);

                    stream.Flush();
                    stream.Close();

                    return image;
                }
            }
            catch
            {
                return null;
            }
            
        }

        #endregion


        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    SecurityService = null;
                    GalleryService = null;

                    CrawlRepository = null;
                    NewsRepository = null;
                    NewsCategoryRepository = null;
                    TagRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}