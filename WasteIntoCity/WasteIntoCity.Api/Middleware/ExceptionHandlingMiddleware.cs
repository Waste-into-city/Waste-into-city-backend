using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private record ErrorResponse
        {
            public string Message { get; init; } = string.Empty;
        }


        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            ErrorResponse? errorResponse = null;

            try
            {
                await _next(context);
            }
            catch (BadRequest400Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = BadRequest400Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Message = ex.Message
                };
            }
            catch (NotFound404Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = NotFound404Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Message = ex.Message
                };
            }
            catch (InternalServer500Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = InternalServer500Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                errorResponse = new ErrorResponse
                {
                    Message = "Error: Unexpected server exception."
                };
            }

            if (errorResponse is not null)
            {
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
