using CarLinePickup.Domain.Exceptions;
using CarLinePickup.Domain.Services;
using CarLinePickup.Domain.Exceptions.ErrorCodes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarLinePickup.API.Middleware
{
    public static class ExceptionHandling
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {                   
                    var exceptionService = new ExceptionService();

                    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
                    var exceptionMessage = string.Empty;
                    var exceptionCode = string.Empty;
                    int statusCode = 0;

                    switch (exception)
                    {
                        case null:
                            return;
                        case BusinessRuleViolationException businessException:
                            statusCode = StatusCodes.Status400BadRequest;
                            exceptionCode = businessException.ErrorCode;
                            exceptionMessage = businessException.Message;
                            break;
                        case KeyNotFoundException keyNotFoundException:
                            statusCode = StatusCodes.Status500InternalServerError;
                            exceptionCode = SystemCodes.InternalError;
                            exceptionMessage = keyNotFoundException.Message;
                            break;
                        default:
                            statusCode = StatusCodes.Status500InternalServerError;
                            exceptionCode = SystemCodes.InternalError;
                            exceptionMessage = "A problem occured while handling your request. Details have been logged.";
                            break;
                    }

                    var response = new { code = exceptionCode, message = exceptionMessage };

                    var payload = JsonConvert.SerializeObject(response);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(payload);
                });
            });

            return builder;
        }
    }
}
