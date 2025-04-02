using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions
{
    public class ForbiddenAccessResource403Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status403Forbidden;

        public const string NAME = "Forbidden access resource";

        protected ForbiddenAccessResource403Exception(string message) : base(message)
        {
        }

        public ForbiddenAccessResource403Exception() : base(NAME)
        {

        }
    }
}
