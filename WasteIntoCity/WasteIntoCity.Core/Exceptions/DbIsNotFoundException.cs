namespace WasteIntoCity.Core.Exceptions
{
    public class DbIsNotFoundException : NotFound404Exception
    {
        protected DbIsNotFoundException(string message) : base(message)
        {
        }

        public DbIsNotFoundException(string entityName, string? message) : base($"Error: entity {entityName} => {message ?? "was not found"}.")
        {
        }

        public DbIsNotFoundException(string entityName, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "was not found"}. {additionMessage}")
        {
        }
    }
}
