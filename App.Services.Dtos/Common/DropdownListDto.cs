using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    /// <summary>
    /// Represents a dropdownlist
    /// </summary>
    public class DropdownListDto : DtoBase
    {
        public int Value { get; set; }
        public string Label { get; set; }
    }
}
