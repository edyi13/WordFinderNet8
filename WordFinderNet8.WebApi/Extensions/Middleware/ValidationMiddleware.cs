using System.Text.Json;
using WordFinder.Application.UseCases.Commons.Bases;
using WordFinder.Application.UseCases.Commons.Exceptions;

namespace WordFinderNet8.WebApi.Extensions.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body, new BaseResponse<object> { Message = "Validation Errors", Errors = ex.Errors });
            }
        }
    }
}
