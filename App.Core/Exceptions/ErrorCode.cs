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
                {ErrorCodeType.DataEmpty, "Data is empty."}
            };
        }
    }
}
