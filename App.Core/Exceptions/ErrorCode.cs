using System.Collections.Generic;

namespace App.Core.Exceptions
{
    public static class ErrorCode
    {
        public static IDictionary<ErrorCodeType, string> ErrorCodes;

        static ErrorCode()
        {
            ErrorCodes = new Dictionary<ErrorCodeType, string>
            {
                {ErrorCodeType.Error, "The data you have requested could not be retreived at this time.  Please try again." },
                {ErrorCodeType.Validation, "Fail validation."},
                {ErrorCodeType.NoPermission, "No permission."},
                {ErrorCodeType.NoAccessData, "You do not have permission to access this data."},
                {ErrorCodeType.DataNotFound, "Data not found."},
                {ErrorCodeType.InvalidData, "Data is not valid."},
                {ErrorCodeType.InvalidName, "Name is not valid."},
                // Role
                {ErrorCodeType.InvalidRoleName, "Role name is not valid."},
                {ErrorCodeType.RoleNameIsUsed, "Role name is used."},
                {ErrorCodeType.RoleIsNotExsted, "Role is not existed."},
                // User
                {ErrorCodeType.InvalidFirstName, "First name is not valid."},
                {ErrorCodeType.InvalidLastName, "Last name is not valid."},
                {ErrorCodeType.InvalidEmail, "Email is not valid."},
                {ErrorCodeType.EmailIsUsed, "Email is used."},
                // Product
            };
        }
    }
}
