using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Exceptions;

namespace App.Services.Dtos.Common
{
    public class ErrorExtraInfodsd
    {
        public ErrorCodeType Code { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
    }
}
