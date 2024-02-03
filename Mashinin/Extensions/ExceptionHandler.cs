using Mashinin.Exceptions;
using Mashinin.Logger;
using Microsoft.AspNetCore.Diagnostics;

namespace Mashinin.Extensions
{
    public static class ExceptionHandler
    {
        public static void ExceptionHandling(this IApplicationBuilder app, ILoggerManager loggerManager)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                    int statuscode = 500;
                    string errormsg = feature.Error.Message ?? "Internal Server Error!";
                    string errorDetails = feature.Error.StackTrace;
                    loggerManager.LogError(errormsg + "\n" + errorDetails);

                    if (feature.Error is NotFoundException)
                    {
                        statuscode = 404;
                        errormsg = feature.Error.Message;
                    }
                    else if (feature.Error is RecordDuplicateException)
                    {
                        statuscode = 409;
                        errormsg = feature.Error.Message;
                    }
                    else if (feature.Error is BadRequestException)
                    {
                        statuscode = 400;
                        errormsg = feature.Error.Message;
                    }

                    context.Response.StatusCode = statuscode;
                    await context.Response.WriteAsync(errormsg);
                });
            });
        }
    }
}