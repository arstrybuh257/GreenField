using GreenField.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GreenField.Api.Extensions
{
    public static class ErrorHandlingExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder, bool isApiApp = false)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>(isApiApp);
        }
    }
}