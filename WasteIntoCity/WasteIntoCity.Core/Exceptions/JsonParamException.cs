namespace WasteIntoCity.Core.Exceptions
{
    public class JsonParamException : InternalServer500Exception
    {
        protected JsonParamException(string message) : base(message)
        {
        }

        public JsonParamException(string argName, string? message) : base($"Error: appsettings.json. {argName} => {message ?? "Cannot find json value from this param"}.")
        {
        }

        public JsonParamException(string argName, string? message, string? fileName) : base($"Error: {fileName ?? "appsettings.json"}. {argName} => {message ?? "Cannot find json value from this param"}.")
        {
        }
    }
}
