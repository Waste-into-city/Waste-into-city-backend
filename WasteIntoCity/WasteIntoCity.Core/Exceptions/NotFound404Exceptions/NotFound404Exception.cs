using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions.NotFound404Exceptions
{
    public class NotFound404Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status404NotFound;

        public const string NAME = "Not found";

        protected NotFound404Exception(string message, int code) : base(message)
        {
        }

        public int Code { get; set; }
    }
}
