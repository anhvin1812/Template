using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions
{
    public sealed class PermissionException : AppException
    {
        public PermissionException()
        {
            ErrorCode = ErrorCodeType.NoPermission;
        }
    }
}