using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCodeType ErrorCode { get; internal set; }
        public bool LogException { get; internal set; }

        public AppException()
            : base()
        {
            // Add implementation (if required)
            ErrorCode = ErrorCodeType.Error;
        }

        public AppException(string message)
            : base(message)
        {
            // Add implementation (if required)
            ErrorCode = ErrorCodeType.Error;
        }

        public AppException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation (if required)
            ErrorCode = ErrorCodeType.Error;
        }

        protected AppException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Add implementation (if required)
            ErrorCode = ErrorCodeType.Error;
        }
    }
}
