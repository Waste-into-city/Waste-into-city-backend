using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Errors
{
    public class InternalServer500Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status500InternalServerError;

        protected InternalServer500Exception(string message) : base(message)
        {
        }
    }
}
