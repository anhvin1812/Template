using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace App.Services.Dtos.Common
{
    public partial class EntityAction
    {
        protected EntityAction()
        {
            Id = Guid.NewGuid().ToString();
            ActionUrl = GetFullUrl("");
        }

        public string Id { get; set; }
        public EntityActionType Code { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string ActionUrl { get; set; }

        public IEnumerable<EntityAction> Children { get; set; }

        protected static string GetFullUrl(string url)
        {
            var baseUrl = ConfigurationManager.AppSettings["WebApiBaseUrl"];
            return string.Format("{0}{1}", baseUrl, url);
        }
    }
}
