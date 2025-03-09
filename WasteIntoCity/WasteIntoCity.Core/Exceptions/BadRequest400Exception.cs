using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Errors
{
    public class BadRequest400Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status400BadRequest;

        public const string NAME = "Bad request";

        protected BadRequest400Exception(string message) : base(message)
        {
        }
    }
}
