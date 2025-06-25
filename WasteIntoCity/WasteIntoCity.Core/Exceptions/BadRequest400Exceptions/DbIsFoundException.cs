namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class DbIsFoundException : BadRequest400Exception
    {
        protected DbIsFoundException(string message, int code) : base(message, code)
        {
        }

        public DbIsFoundException(string entityName, string? message, int code) : base($"Error: entity {entityName} => {message ??
            "already exists"}.", code)
        {
        }

        public DbIsFoundException(string entityName, string? message, string additionMessage, int code)
            : base($"Error: entity {entityName} => {message ?? "already exists"}. {additionMessage}", code)
        {
        }
    }
}
