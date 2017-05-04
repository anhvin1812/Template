using System.Collections;
using System.Collections.Generic;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.UI
{
    public class SelectListOptions : DtoBase
    {
        public SelectListOptions()
        {
            DataValueField = "Value";
            DataTextField = "Text";
        }
        public IEnumerable<OptionItem> Items { get; set; } 
        public IEnumerable DisabledValues { get; set; }
        public IEnumerable<int> SelectedValues { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
    }

    public class OptionItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

}
