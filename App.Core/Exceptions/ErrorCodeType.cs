using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions
{
    public enum ErrorCodeType
    {
        Error = 1,
        Validation = 2,
        NoPermission = 3,
        NoAccessData = 4,
        DataNotFound = 5,
        InvalidData = 6,
        
        // Role
        InvalidRoleName = 7,
        RoleNameIsUsed = 8,
        RoleIsNotExsted = 9,
    }
}
