using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Exceptions;
using App.Services.Dtos.Common;

namespace App.Website.Fillters
{
    public class ErrorHandlerAttribute : HandleErrorAttribute
    {
        //private ILog _logger;

        public ErrorHandlerAttribute()
        {
           // _logger = Log4NetManager.GetLogger("MyLogger");
        }

        public override void OnException(ExceptionContext context)
        {
            var logException = true;

            // Create error
            var error = new Error
            {
                Code = ErrorCodeType.Error,
                Message = ErrorCode.ErrorCodes[ErrorCodeType.Error]
            };
            
            // Create viewData
            ViewDataDictionary viewData = context.Controller.ViewData;
            viewData.Model = context.Controller.TempData["model"];

            if (context.Exception is AppException)
            {
                var exp = context.Exception as AppException;
              
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
                                viewData.ModelState.AddModelError(extraInfo.Code.ToString(),
                                    ErrorCode.ErrorCodes[extraInfo.Code]);

                                error.ExtraInfos.Add(new ErrorExtraInfo
                                {
                                    Code = extraInfo.Code,
                                    Message = ErrorCode.ErrorCodes[extraInfo.Code],
                                    Property = extraInfo.Property
                                });
                            }
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(context.Exception.Message))
                {
                    viewData.ModelState.AddModelError(ErrorCodeType.Error.ToString(), context.Exception.Message);
                    error.ExtraInfos.Add(new ErrorExtraInfo { Code = ErrorCodeType.Error, Message = context.Exception.Message });
                }   
            }

            if (IsAjax(context))
            {
                context.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new FailResult(error)
                };
                context.ExceptionHandled = true;
                context.HttpContext.Response.Clear();
            }
            else
            {
                if(IsGlobalError(context.Exception))
                    return;

                context.Result = new ViewResult
                {
                    MasterName = this.Master ?? "_Layout",
                    ViewName = this.View,
                    ViewData = viewData,
                    TempData = context.Controller.TempData
                };

                context.ExceptionHandled = true;
            }
        }

        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        private bool IsGlobalError(Exception ex)
        {
            return !(ex is ValidationError);
        }


    }
}