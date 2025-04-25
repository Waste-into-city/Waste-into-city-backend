using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class InternalServer500Exception : Exception
    {
        public const int STATUS_CODE = StatusCodes.Status500InternalServerError;

        public const string NAME = "Internal server error";

        protected InternalServer500Exception(string message, int code) : base(message)
        {
            Code = STATUS_CODE * 1000 + code;
        }

        public int Code { get; set; }
    }
}
