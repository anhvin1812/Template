using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions
{
    public sealed class DataNotFoundException : AppException
    {
        public DataNotFoundException()
        {
            ErrorCode = ErrorCodeType.DataNotFound;
        }
    }
}