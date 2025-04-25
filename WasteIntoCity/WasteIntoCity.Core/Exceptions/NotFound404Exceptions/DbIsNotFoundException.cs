namespace WasteIntoCity.Core.Exceptions.NotFound404Exceptions
{
    public class DbIsNotFoundException : NotFound404Exception
    {
        protected DbIsNotFoundException(string message, int code) : base(message, code)
        {
        }

        public DbIsNotFoundException(string entityName, int code, string? message)
            : base($"Error: entity {entityName} => {message ?? "was not found"}.", code)
        {
        }

        public DbIsNotFoundException(string entityName, int code, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "was not found"}. {additionMessage}", code)
        {
        }
    }
}
