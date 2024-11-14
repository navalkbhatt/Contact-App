using ContactApp.Application.UseCases.Commons;
using ContactApp.Application.UseCases.Exceptions;
using System.Text.Json;

namespace ContactAppApi.Extensions.Middleware
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
                await JsonSerializer.SerializeAsync(context.Response.Body, new Response<object> { Body = "Validation Errors", Errors = ex.Errors });
            }
        }
    }
}
