using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.ForbiddenAccessResource403Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions;

namespace WasteIntoCity.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        private readonly Dictionary<int, string> exceptionMessages = new Dictionary<int, string>
        {
            { BadRequest400Exception.STATUS_CODE, BadRequest400Exception.NAME},
            { Unauthorized401Exception.STATUS_CODE, Unauthorized401Exception.NAME },
            { ForbiddenAccessResource403Exception.STATUS_CODE, ForbiddenAccessResource403Exception.NAME },
            { InternalServer500Exception.STATUS_CODE, InternalServer500Exception.NAME }
        };

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
                    Code = ex.Code,
                    Message = ex.Message
                };
            }
            catch (Unauthorized401Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = Unauthorized401Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Code = ex.Code,
                    Message = ex.Message
                };
            }
            catch (ForbiddenAccessResource403Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = ForbiddenAccessResource403Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Code = ex.Code,
                    Message = ex.Message
                };
            }
            catch (NotFound404Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = NotFound404Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Code = ex.Code,
                    Message = ex.Message
                };
            }
            catch (InternalServer500Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = InternalServer500Exception.STATUS_CODE;

                errorResponse = new ErrorResponse
                {
                    Code = ex.Code,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                errorResponse = new ErrorResponse
                {
                    Code = InternalServer500Exception.STATUS_CODE * 1000 + 997,
                    Message = "Error: Unexpected server exception."
                };
            }

            if (errorResponse is not null)
            {
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
            else if (context.Response.StatusCode >= 400)
            {
                if (exceptionMessages.TryGetValue(context.Response.StatusCode, out string? message))
                {
                    errorResponse = new ErrorResponse
                    {
                        Code = context.Response.StatusCode * 1000 + 998,
                        Message = message
                    };
                }
                else
                {
                    errorResponse = new ErrorResponse
                    {
                        Code = context.Response.StatusCode * 1000 + 999,
                        Message = "Error: Unexpected server exception."
                    };
                }

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
