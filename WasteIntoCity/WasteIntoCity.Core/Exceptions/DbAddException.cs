namespace WasteIntoCity.Core.Exceptions
{
    public class DbAddException : InternalServer500Exception
    {
        protected DbAddException(string message) : base(message)
        {
        }

        public DbAddException(string entityName, string? message) : base($"Error: entity {entityName} => {message ?? "Failed adding row"}.")
        {
        }

        public DbAddException(string entityName, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "failed adding row"}. {additionMessage}")
        {
        }
    }
}
