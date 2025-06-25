namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class JsonParamException : InternalServer500Exception
    {
        protected JsonParamException(string message, int code) : base(message, code)
        {
        }

        public JsonParamException(string argName, string? message, int code)
            : base($"Error: appsettings.json. {argName} => {message ?? "Cannot find json value from this param"}.", code)
        {
        }

        public JsonParamException(string argName, string? message, string? fileName, int code)
            : base($"Error: {fileName ?? "appsettings.json"}. {argName} => {message ?? "Cannot find json value from this param"}.", code)
        {
        }
    }
}
