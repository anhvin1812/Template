using System;
using System.Collections;
using System.Collections.Generic;
using App.Entities.FileManagement;
using App.Entities.IdentityManagement;

namespace App.Entities.NewsManagement
{
    public class CrawlSourcePage : EntityBase
    {
        public int Id { get; set; }
        public int CrawlSourceId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsRelativeUrl { get; set; }
        public string BaseUrl { get; set; }

        public virtual CrawlSource CrawlSource { get; set; }

    }

}
