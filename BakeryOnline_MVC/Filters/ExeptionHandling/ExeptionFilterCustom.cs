using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BakeryOnline_MVC.Filters.ExeptionHandling
{
    public class ExeptionFilterCustom : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
           
                var exeption = context.Exception;
                var message = exeption.Message;

                var errModel = new ErrorViewModel()
                {
                    RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                    ErrMessage = message,
                };

                context.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<ErrorViewModel>(new EmptyModelMetadataProvider(), context.ModelState)
                    {
                        Model = errModel
                    }
                };

                context.ExceptionHandled = true;
            
           
            return Task.CompletedTask;
        }
    }
}
