using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using App.Core.Exceptions;
using App.Services.Dtos.Common;
using ErrorCodeType = App.Core.Exceptions.ErrorCodeType;
using ErrorExtraInfo = App.Core.Exceptions.ErrorExtraInfo;

namespace App.Website.Fillters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var logException = true;
            //create error
            var error = new Error
            {
                Code = ErrorCodeType.Error,
                Message = ErrorCode.ErrorCodes[ErrorCodeType.Error]
            };

            if (context.Exception is AppException)
            {
                var exp = context.Exception as AppException;
                error.Code = exp.ErrorCode;
                error.Message = ErrorCode.ErrorCodes[error.Code];
                logException = exp.LogException;

                if (context.Exception is ValidationError)
                {
                    foreach (DictionaryEntry de in exp.Data)
                    {
                        if (de.Value is List<ErrorExtraInfo>)
                        {
                            var extraInfos = de.Value as List<ErrorExtraInfo>;
                            foreach (var extraInfo in extraInfos)
                            {
                                context.ActionContext.ModelState.AddModelError(extraInfo.Code.ToString(), ErrorCode.ErrorCodes[extraInfo.Code]);
                                //error.ExtraInfos.Add(new ErrorExtraInfo
                                //{
                                //    Code = extraInfo.Code,
                                //    Message = ErrorCode.ErrorCodes[extraInfo.Code],
                                //    Property = extraInfo.Property
                                //});
                            }
                        }
                    }
                }
            }
            else
            {
                //if (!String.IsNullOrEmpty(context.Exception.Message))
                //{
                //    context.ActionContext.ModelState.AddModelError(ErrorCodeType.Error.ToString(), ErrorCode.ErrorCodes[extraInfo.Code]);
                //    error.ExtraInfos.Add(new ErrorExtraInfo { Code = ErrorCodeType.Error, Message = context.Exception.Message });
                //}
                
            }

            context.ActionContext.ModelState.AddModelError(ErrorCodeType.Error.ToString(), context.Exception.Message);

            //if (logException)
            // Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(context.Exception));

            //context.Response = context.Request.CreateResponse(HttpStatusCode.OK, new FailResult(error));
            //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(context.Exception));
        }
    }
}