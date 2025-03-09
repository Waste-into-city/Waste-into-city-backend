namespace WasteIntoCity.Core.Errors
{
    public class DbUpdateException : InternalServer500Exception
    {
        protected DbUpdateException(string message) : base(message)
        {
        }

        public DbUpdateException(string entityName, string? message) : base($"Error: entity {entityName} => {message ?? "was not found"}.")
        {
        }

        public DbUpdateException(string entityName, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "was not found"}. {additionMessage}")
        {
        }
    }
}
