using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Errors
{
    public class NotFound404Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status404NotFound;

        public const string NAME = "Not found";

        protected NotFound404Exception(string message) : base(message)
        {
        }
    }
}
