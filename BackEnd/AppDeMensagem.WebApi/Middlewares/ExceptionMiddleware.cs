

using AppDeMensagem.Application.DTOs.ResponseApi;

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
