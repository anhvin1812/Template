using System.Collections;
using System.Collections.Generic;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.UI
{
    public class SelectListOptions : DtoBase
    {
        public IEnumerable<OptionItem> Items { get; set; } 
        public IEnumerable DisabledValues { get; set; }
        public IEnumerable<int> SelectedValues { get; set; }
        public string DataTextField => "Text";
        public string DataValueField => "Value";
    }

    public class OptionItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

}
