namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class DbUpdateCustomException : InternalServer500Exception
    {
        protected DbUpdateCustomException(string message, int code) : base(message, code)
        {
        }

        public DbUpdateCustomException(string entityName, int code, string? message) :
            base($"Error: entity {entityName} => {message ?? "was not found"}.", code)
        {
        }

        public DbUpdateCustomException(string entityName, int code, string? message, string additionMessage)
            : base($"Error: entity {entityName} => {message ?? "was not updated"}. {additionMessage}", code)
        {
        }
    }
}
