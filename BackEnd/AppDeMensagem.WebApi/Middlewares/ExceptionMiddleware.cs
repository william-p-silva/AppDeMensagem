

using AppDeMensagem.Application.DTOs.ResponseApi;
using AppDeMensagem.Domain.Exceptions;

namespace AppDeMensagem.WebApi.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (BusinessRuleException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (ValidationException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (NotFoundException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (DomainException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (InvalidOperationException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (ArgumentNullException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (ArgumentException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (UnauthorizedAccessException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new ErrorResponse
                {
                    Message = ex.Message,
                    Success = false,
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
