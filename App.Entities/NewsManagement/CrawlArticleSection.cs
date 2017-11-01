using System;
using System.Collections;
using System.Collections.Generic;
using App.Entities.FileManagement;
using App.Entities.IdentityManagement;

namespace App.Entities.NewsManagement
{
    public class CrawlArticleSection : EntityBase
    {
        public int Id { get; set; }
        public int CrawlSourcePageId { get; set; }
        public string Name { get; set; }
        public string Selector { get; set; }
        public string TitleSelector { get; set; }
        public string LinkSelector { get; set; }
        public string DescriptionSelector { get; set; }
        public string FeaturedImageSelector { get; set; }
        public string FeaturedImageAttribute { get; set; }
        public string FeatureImageSizeIdentity { get; set; }
        public string LargeFeatureImageSizeIdentity { get; set; }
        public bool IsRelativeUrl { get; set; }
        public string BaseUrl { get; set; }

        public virtual CrawlSourcePage CrawlSourcePage { get; set; }
    }

}
