using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using App.Website.Fillters;

namespace App.Website.App_Start
{
    public static class ExceptionHandlingConfig
    {
        /// <summary>
        /// Registers the exception handler and loggers.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void RegisterExceptionHandler(HttpConfiguration configuration)
        {
            //  configuration.Services.Replace(typeof(IExceptionHandler), new ServiceExceptionHandler());
            //    configuration.Services.Add(typeof(IExceptionLogger), new ServiceExceptionLogger(new NullLogger()));
            configuration.Filters.Add(new UnhandledExceptionFilter());
        }


    }
}