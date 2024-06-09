using gestao_produtos.api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace gestao_produtos.api.Middlewares
{
    public class ErrorHandlingMiddleware : AbstractExceptionHandlerMiddleware
    {
        private readonly IHostEnvironment _environment;
        private const string _errorMessage = "An internal error occurred while processing your request.";
        private readonly string _apiResponseJson;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger,
            IHostEnvironment environment) : base(next, logger)
        {
            _environment = environment;

            _apiResponseJson = JsonSerializer.Serialize(ApiResponse.InternalServerError(_errorMessage));
        }

        public override string GetResponse(Exception exception)
        {
            ApiResponse response = new();
            switch (exception)
            {
                case KeyNotFoundException
                    or FileNotFoundException:
                    response = ApiResponse.NotFound();
                    break;
                case UnauthorizedAccessException:
                    response = ApiResponse.Unauthorized();
                    break;
                case ArgumentException
                    or InvalidOperationException:
                    response = ApiResponse.BadRequest();
                    break;
                default:
                    response = ApiResponse.InternalServerError(exception.Message);
                    break;
            }
            return JsonSerializer.Serialize(response);
        }
    }
}
