namespace WasteIntoCity.Core.Exceptions.ForbiddenAccessResource403Exceptions
{
    public class UnhonestUserAccessDeniedException : ForbiddenAccessResource403Exception
    {
        public const string DEFAULT_MESSAGE = "Error => Unhonest user, denied access to resource";

        public UnhonestUserAccessDeniedException(string? message, int code) : base(message ?? DEFAULT_MESSAGE, code)
        {
        }
    }
}
