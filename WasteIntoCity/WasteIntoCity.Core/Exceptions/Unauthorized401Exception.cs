using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions
{
    public class Unauthorized401Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status401Unauthorized;

        public const string NAME = "Unauthorized";

        protected Unauthorized401Exception(string message) : base(message)
        {
        }
    }
}
