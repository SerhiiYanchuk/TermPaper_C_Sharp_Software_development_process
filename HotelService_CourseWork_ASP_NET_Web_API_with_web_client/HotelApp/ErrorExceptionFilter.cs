using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelApp
{
    public class ErrorExceptionFilter: Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string actionName = context.ActionDescriptor.DisplayName;
            string exceptionStack = context.Exception.StackTrace;
            string exceptionMessage = context.Exception.Message;
            string value = $"В методе {actionName} возникло исключение: \n {exceptionMessage} \n {exceptionStack}";

            ObjectResult ex = new ObjectResult(value);
            ex.StatusCode = 500;
            ex.ContentTypes.Add("application/json");           
            context.Result = ex;

            //context.ExceptionHandled = true;
        }
    }
}
