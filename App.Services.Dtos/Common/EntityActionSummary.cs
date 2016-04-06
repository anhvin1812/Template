using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    public class EntityActionSummary
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string ActionUrl { get; set; }
    }
}
