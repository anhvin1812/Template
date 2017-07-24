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
        InvalidName = 7,
        
        // Role
        InvalidRoleName = 20,
        RoleNameIsUsed = 21,
        RoleIsNotExsted = 22,

        // Product
        InvalidProductCategoryName = 30,

        // User
        InvalidFirstName = 100,
        InvalidLastName = 101,
        InvalidEmail = 102,
        EmailIsUsed = 103,

        // News
        InvalidTitle = 200,
        NewsCategoryIsEmpty = 201,
        EmptyImage = 202,
        NewsStatusIsNotExisted = 203,

        // Tag
        TagNameIsExisted = 300,

        // Settings
        
    }
}
