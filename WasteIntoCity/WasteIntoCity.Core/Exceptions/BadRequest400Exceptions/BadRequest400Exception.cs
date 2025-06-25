using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class BadRequest400Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status400BadRequest;

        public const string NAME = "Bad request";

        protected BadRequest400Exception(string message, int code) : base(message)
        {
            Code = STATUS_CODE * 1000 + code;
        }

        public int Code { get; set; }
    }
}
