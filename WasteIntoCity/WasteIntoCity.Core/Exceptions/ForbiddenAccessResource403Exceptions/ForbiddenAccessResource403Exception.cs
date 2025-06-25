using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions.ForbiddenAccessResource403Exceptions
{
    public class ForbiddenAccessResource403Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status403Forbidden;

        public const string NAME = "Forbidden access resource";

        protected ForbiddenAccessResource403Exception(string message, int code) : base(message)
        {
            Code = STATUS_CODE * 1000 + code;
        }

        public int Code { get; set; }
    }
}
