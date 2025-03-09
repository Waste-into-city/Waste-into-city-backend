namespace WasteIntoCity.Core.Errors
{
    public class DbIsFoundException : BadRequest400Exception
    {
        protected DbIsFoundException(string message) : base(message)
        {
        }

        public DbIsFoundException(string entityName, string? message) : base($"Error: entity {entityName} => {message ?? "already exists"}.")
        {
        }

        public DbIsFoundException(string entityName, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "already exists"}. {additionMessage}")
        {
        }
    }
}
