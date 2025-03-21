namespace WasteIntoCity.Core.Exceptions
{
    public class DbUpdateCustomException : InternalServer500Exception
    {
        protected DbUpdateCustomException(string message) : base(message)
        {
        }

        public DbUpdateCustomException(string entityName, string? message) : base($"Error: entity {entityName} => {message ?? "was not found"}.")
        {
        }

        public DbUpdateCustomException(string entityName, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "was not updated"}. {additionMessage}")
        {
        }
    }
}
