using System;
using System.Collections;
using System.Collections.Generic;
using App.Entities.FileManagement;
using App.Entities.IdentityManagement;

namespace App.Entities.NewsManagement
{
    public class CrawlSource : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
