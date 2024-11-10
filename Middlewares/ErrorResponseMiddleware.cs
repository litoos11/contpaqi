using System.Net;
using Contpaqi.Entities;

namespace contpaqi.Middlewares
{
    public static class ErrorResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorResponse(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorResponseMiddleware>();
        }
    }
    public class ErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var originalStatus = httpContext.Response.StatusCode;

            await _next(httpContext);

            //TODO: Se puede desglosar cada uno de los status :-(
            if (httpContext.Response.StatusCode == 401)
            {
                httpContext.Response.ContentType = "application/json";
                ErrorResponse errorResponse = new()
                {
                    Status = HttpStatusCode.Unauthorized.GetHashCode(),
                    Title = "Unauthorized",
                    Detail = "El token de acceso ha caducado o no es válido."
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            else if (httpContext.Response.StatusCode == 403)
            {
                httpContext.Response.ContentType = "application/json";
                ErrorResponse errorResponse = new()
                {
                    Status = HttpStatusCode.Forbidden.GetHashCode(),
                    Title = "Forbidden",
                    Detail = "No tienes permiso para acceder a este recurso."
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
            else if (httpContext.Response.StatusCode >= 500 && httpContext.Response.StatusCode < 600)
            {
                httpContext.Response.ContentType = "application/json";
                ErrorResponse errorResponse = new()
                {
                    Status = HttpStatusCode.InternalServerError.GetHashCode(),
                    Title = "Internal Server Error",
                    Detail = "Se produjo un error inesperado. Inténtelo de nuevo más tarde."
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}