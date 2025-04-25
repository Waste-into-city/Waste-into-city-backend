using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions
{
    public class Unauthorized401Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status401Unauthorized;

        public const string NAME = "Unauthorized";

        protected Unauthorized401Exception(string message, int code) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}
