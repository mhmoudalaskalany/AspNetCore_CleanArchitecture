using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Template.Common.Core;
using Template.Common.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace Template.Api.MiddleWares
{
    /// <summary>
    /// Exception Middleware
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        /// <summary>
        /// Invoke
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handle
        /// </summary>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {


            var serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var exception = new
            {
                ex.Message,
                ex.StackTrace,
                ex.InnerException
            };

            var exceptionJson = JsonConvert.SerializeObject(exception, serializerSettings);

            context.Response.ContentType = "application/json";

            var detailedExceptionMessage = $"----------Exception---------{exceptionJson}---------";
            Log.ForContext("Message", exception.Message)
            .Error(detailedExceptionMessage);

            if (ex is BaseException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult() { Message = ex.Message }));
            }
            else if (ex is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult
                {
                    Message = "Unauthorized",
                    Status = HttpStatusCode.Unauthorized
                }));
            }
            else if (ex is DbUpdateException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if (ex.InnerException != null)
                {
                    var dbException = (SqlException)ex.InnerException;

                    switch (dbException.Number)
                    {
                        case 547:
                            {
                                var table = dbException.Message.Split("table");
                                var column = table[1].Split("column");
                                var error = new FinalResult
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = $"Wrong Foreign Key (Id) For Entity {column[0]}"
                                };

                                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                                break;
                            }
                        default:
                            {

                                var error = new FinalResult
                                {
                                    Status = HttpStatusCode.BadRequest,
                                    Message = dbException.Message
                                };
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                                break;
                            }
                    }
                }
                else
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult() { Message = ex.Message }.ToString()));
                }


            }
            else
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseResult()
                {
                    Message = _configuration["Enable_Stack_Trace"] == "TRUE" ? exceptionJson : ex.Message

                }));
            }
        }
    }
}


