using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using App.Core.DataModels;

namespace App.Website.Common
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString TreeMenu(this IEnumerable<ICategory> categories, string id, string cssClass, string linkFormat, int? rootId = null, int? selectedValue = null)
        {
            if(categories == null || !categories.Any())
                return new MvcHtmlString(string.Empty);

            var tags = GenerateTreeMenu(categories, id, cssClass, linkFormat, rootId, selectedValue);

            return new MvcHtmlString(tags.ToString(TagRenderMode.Normal));
        }

        private static TagBuilder GenerateTreeMenu(IEnumerable<ICategory> categories, string id, string cssClass, string linkFormat, int? rootId = null, int? selectedValue = null)
        {
            var ul = new TagBuilder("ul");
            ul.GenerateId(id);
            ul.MergeAttribute("class", cssClass);

            var parentCategories = categories.Where(x => x.ParentId == rootId);
            if (parentCategories.Any())
            {
                foreach (var c in parentCategories)
                {
                    var li = new TagBuilder("li");

                    if (c.Id == selectedValue)
                        li.AddCssClass("active");

                    var a = new TagBuilder("a");
                    a.SetInnerText(c.Name);
                    a.MergeAttribute("href", string.Format(linkFormat, c.Id) );

                    li.InnerHtml = a.ToString(TagRenderMode.Normal);

                    if (categories.Any(x => x.ParentId == c.Id))
                    {
                        li.AddCssClass("treeview");
                        var subMenu = GenerateTreeMenu(categories, string.Empty, "treeview-menu", linkFormat, c.Id, selectedValue).ToString(TagRenderMode.Normal);
                        li.InnerHtml = $"{li.InnerHtml}{subMenu}";
                    }
                   

                    ul.InnerHtml = $"{ul.InnerHtml}{li.ToString(TagRenderMode.Normal)}"; 
                }
            }
            
            return ul; 
        }
    }
}