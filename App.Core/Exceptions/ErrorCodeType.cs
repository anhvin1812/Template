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
        DataEmpty = 5,
        
        // Role
        InvalidRoleName = 6,
        RoleNameIsUsed = 7,
        RoleIsNotExsted = 8,
    }
}
