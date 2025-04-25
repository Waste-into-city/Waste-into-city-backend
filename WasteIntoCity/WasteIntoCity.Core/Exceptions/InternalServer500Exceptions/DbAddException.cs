namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class DbAddException : InternalServer500Exception
    {
        protected DbAddException(string message, int code) : base(message, code)
        {
        }

        public DbAddException(string entityName, int code, string? message) : base($"Error: entity {entityName} => {message ??
            "Failed adding row"}.", code)
        {
        }

        public DbAddException(string entityName, int code, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "failed adding row"}. {additionMessage}", code)
        {
        }
    }
}
