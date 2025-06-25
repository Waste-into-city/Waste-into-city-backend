namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class UnknownRoleException : InternalServer500Exception
    {
        public UnknownRoleException(int code, string entityName, string? message)
            : base($"Error: entity {entityName} => {message ?? "Unknown role"}.", code)
        {
        }
    }
}
