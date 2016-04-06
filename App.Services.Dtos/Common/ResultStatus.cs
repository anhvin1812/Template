using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    public abstract class ResultStatus
    {
        public ResultStatusType Code { get; set; }
        public string Description { get; set; }
    }
}
