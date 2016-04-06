using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    public class SuccessStatus : ResultStatus
    {
        public SuccessStatus()
        {
            Code = 0;
            Description = "success";
        }
    }
}
