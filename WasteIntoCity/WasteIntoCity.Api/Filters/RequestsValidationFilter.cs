using Microsoft.AspNetCore.Mvc.Filters;
using WasteIntoCity.Core.Exceptions;

namespace WasteIntoCity.Api.Filters
{
    public class RequestsValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new IncorrectRequestFormatException(context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
