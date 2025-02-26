namespace WasteIntoCity.Core.Errors
{
    public class JsonParamException : Exception
    {
        protected JsonParamException(string message) : base(message)
        {
        }

        public JsonParamException(string argName, string? message) : base($"appsettings.json error. {argName} => {message ?? "Cannot find json value from this param"}.")
        {
        }

        public JsonParamException(string argName, string? message, string? fileName) : base($"{fileName ?? "appsettings.json"} error. {argName} => {message ?? "Cannot find json value from this param"}.")
        {
        }
    }
}
