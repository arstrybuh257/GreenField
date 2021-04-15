using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DuckLab.Common.ErrorHandling;
using GreenField.Api.Models.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GreenField.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly bool _isApiApp;

        public ErrorHandlerMiddleware(RequestDelegate next, 
            ILogger<ErrorHandlerMiddleware> logger, bool isApiApp)
        {
            _next = next;
            _logger = logger;
            _isApiApp = isApiApp;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await HandleErrorAsync(context, exception, _isApiApp);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception, bool isApiApp)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            
            object response;
            if (isApiApp)
            {
                var errors = new List<ErrorModel>()
                {
                    new ErrorModel(errorCode, exception.Message)
                };

                response = new ApiErrorResponse()
                {
                    Errors = errors
                };
            }
            else
            {
                response = new ErrorModel(errorCode, exception.Message);
            }

            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            
            var payload = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}