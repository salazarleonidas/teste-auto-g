using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace gestao_produtos.api.Middlewares
{
    public abstract class AbstractExceptionHandlerMiddleware
    {
        // Enrich is a custom extension method that enriches the Serilog functionality - you may ignore it
        protected ILogger _logger;

        /// <summary>
        /// This key should be used to store the exception in the <see cref="IDictionary{TKey,TValue}"/> of the exception data,
        /// to be localized in the abstract handler.
        /// </summary>
        public static string LocalizationKey => "LocalizationKey";

        protected readonly RequestDelegate _next;

        /// <summary>
        /// Gets HTTP status code response and message to be returned to the caller.
        /// Use the ".Data" property to set the key of the messages if it's localized.
        /// </summary>
        /// <param name="exception">The actual exception</param>
        /// <returns>Tuple of HTTP status code and a message</returns>
        public abstract string GetResponse(Exception exception);

        public AbstractExceptionHandlerMiddleware(RequestDelegate next
            , ILogger<AbstractExceptionHandlerMiddleware> logger)
            => (_next, _logger) = (next, logger);

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                // log the error
                _logger.LogError(exception, "error during executing {Context}", context.Request.Path.Value);
                var response = context.Response;
                response.ContentType = "application/json";

                // get the response code and message
                var message = GetResponse(exception);
                await response.WriteAsync(message);
            }
        }
    }
}
