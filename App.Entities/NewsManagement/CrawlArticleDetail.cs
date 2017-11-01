using System;
using System.Collections;
using System.Collections.Generic;
using App.Entities.FileManagement;
using App.Entities.IdentityManagement;

namespace App.Entities.NewsManagement
{
    public class CrawlSourcePageDetail : EntityBase
    {
        public int Id { get; set; }
        public int CrawlSourceId { get; set; }
        public string TitleSelector { get; set; }
        public string DescriptionSelector { get; set; }
        public string ContentSelector { get; set; }
        public string RemoveFromContentSelector { get; set; }
        public string DateSelector { get; set; }
        public string DateFormat { get; set; }
        public string EditorSelector { get; set; }
        public string CategorySelector { get; set; }
        public string TagSelector { get; set; }
        public string VideoSelector { get; set; }
        public string VideoSourceSelector { get; set; }
        public string BaseUrl { get; set; }

        public virtual CrawlSource CrawlSource { get; set; }
    }

}
