using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.Exceptions
{
    public class UnknownRoleException : InternalServer500Exception
    {
        protected UnknownRoleException(string message) : base(message)
        {
        }

        public UnknownRoleException(string entityName, string? message) : base($"Error: entity {entityName} => {message ?? "Unknown role"}.")
        {
        }
    }
}
