using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Errors
{
    public class InternalServer500Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status500InternalServerError;

        public const string NAME = "Internal server error";

        protected InternalServer500Exception(string message) : base(message)
        {
        }
    }
}
