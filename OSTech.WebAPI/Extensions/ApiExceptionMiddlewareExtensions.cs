using Microsoft.AspNetCore.Diagnostics;
using OSTech.Domain.Exceptions;
using System.Text.Json;

namespace OSTech.WebAPI.Extensions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();

                    if (feature == null)
                        return;

                    context.Response.ContentType = "application/json";

                    var error = new ErrorDetails
                    {
                        Message = feature.Error.Message
                    };

                    context.Response.StatusCode = feature.Error switch
                    {
                        NotFoundDomainException => StatusCodes.Status404NotFound,
                        DomainException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    error.StatusCode = context.Response.StatusCode;

#if DEBUG
                    error.Trace = feature.Error.StackTrace;
#endif

                    await context.Response.WriteAsync(error.ToString());
                });
            });
        }
    }
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Trace { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}