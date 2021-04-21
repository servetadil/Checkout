using Checkout.PaymentGateway.Helper.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public ErrorHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = StatusCodes.Status500InternalServerError;
            var message = ex.Message;

            _logger.Error(ex.ToString());
             
            switch (ex)
            {
                case SecurityTokenExpiredException _:
                    code = StatusCodes.Status401Unauthorized;
                    message = ex.Message;
                    break;
                case BadRequestException _:
                    code = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;
                case AuthenticationFailException _:
                    code = StatusCodes.Status401Unauthorized;
                    message = ex.Message;
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}
