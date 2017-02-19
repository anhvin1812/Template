using System.Collections;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.UI
{
    public class SelectListOptions : DtoBase
    {
        public IEnumerable Items { get; set; } 
        public IEnumerable DisabledValues { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
    }
}
