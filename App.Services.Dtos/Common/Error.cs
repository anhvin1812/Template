using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Exceptions;

namespace App.Services.Dtos.Common
{
    public class Error
    {
        public ErrorCodeType Code { get; set; }

        public string Message { get; set; }

        public List<ErrorExtraInfo> ExtraInfos { get; set; }

        public Error()
        {
            ExtraInfos = new List<ErrorExtraInfo>();
        }
    }
}
