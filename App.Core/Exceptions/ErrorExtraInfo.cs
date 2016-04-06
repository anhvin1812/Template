using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions
{
    public class ErrorExtraInfo
    {
        public ErrorCodeType Code { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
    }
}
