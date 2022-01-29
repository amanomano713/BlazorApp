using System;
using System.Net;
using System.Threading.Tasks;
using GOfit.MyGOfit.ExceptionMiddleware.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GOfit.MyGOfit.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private const string ContentType = "application/json";
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (MyGOfitException exception)
            {
                httpContext.Response.ContentType = ContentType;

                if (exception.Type == ExceptionType.Validation ||
                    exception.Type == ExceptionType.Business ||
                    exception.Type == ExceptionType.Data)
                {
                    _logger.LogWarning(exception, "Handled exception.");
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    _logger.LogError(exception, $"Conflict exception. Exception is not of one of this types: {ExceptionType.Validation}, {ExceptionType.Business} or {ExceptionType.Data}");
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                }

                await httpContext.Response.WriteAsync(FormatResponse(exception)).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "Unhandled exception. Details:\n{@Exception}", exception);
                httpContext.Response.ContentType = ContentType;
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpContext.Response.WriteAsync(FormatResponse(exception)).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Format and serialize Viena Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string FormatResponse(MyGOfitException ex) => JsonConvert.SerializeObject(ExceptionBuilder.BuildException(ex.Type, ex.Exception, ex.Entity, ex.Detail, null));

        /// <summary>
        /// Format and serialize Framework Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static string FormatResponse(Exception ex) => JsonConvert.SerializeObject(ExceptionBuilder.BuildException(ExceptionType.Framework, ExceptionRepository.Unknown, ex.Message, null));
    }
}
