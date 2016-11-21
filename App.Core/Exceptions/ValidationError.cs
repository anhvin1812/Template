using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions
{
    public sealed class ValidationError : AppException
    {
        public ValidationError(List<ErrorExtraInfo> violations)
        {
            ErrorCode = ErrorCodeType.Validation;
            Data.Add("ValidationErrors", violations);
        }
    }
}