using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    internal static class ErrorExtentions
    {
        //public static Exception ToException(this FailResult failResult)
        //{
        //    var exception = failResult.Errors.Count == 1 ? failResult.Errors[0].ToException() :
        //        new AggregateException(failResult.Errors.Select(error => error.ToException()).ToList());
        //    return exception;
        //}

        //public static Exception ToException(this Error error)
        //{
        //    Exception exception;
        //    switch (error.Code)
        //    {
        //        case ErrorCodeType.Validation:
        //            var extraInfos = new List<Core.Exceptions.ErrorExtraInfo>();
        //            foreach (var extraInfo in error.ExtraInfos)
        //            {
        //                extraInfos.Add(new Core.Exceptions.ErrorExtraInfo
        //                {
        //                    Code = (Core.Exceptions.ErrorCodeType)extraInfo.Code,
        //                    Property = extraInfo.Property
        //                });
        //            }
        //            exception = new ValidationError(extraInfos);
        //            break;
        //        default:
        //            exception = new Exception(error.Message);
        //            break;
        //    }
        //    return exception;
        //}
    }
}
