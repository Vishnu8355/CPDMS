

using CPDMS.Model.ExceptionList;
using CPDMSEF;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CPDMS.ExceptionHandlear
{
    public class ExceptionFilterHandlear : ExceptionFilterAttribute
    {
        IExceptionProvider _objIExceptionProvider;
        public ExceptionFilterHandlear(IExceptionProvider objIExceptionProvider)
        {
            _objIExceptionProvider = objIExceptionProvider;
        }
        public override void OnException(ExceptionContext context)
        
        {
            try
            {
                LogEntry objLogEntry = new LogEntry();
                objLogEntry.Action = context.ActionDescriptor.DisplayName;
                objLogEntry.Controller = context.HttpContext.Request.Path;
                objLogEntry.ReturnType = "CPDMS";
                objLogEntry.StackTrace = context.Exception.StackTrace;
                objLogEntry.ErrorMessage = context.Exception.Message;

                _objIExceptionProvider.ErrorList(objLogEntry);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
