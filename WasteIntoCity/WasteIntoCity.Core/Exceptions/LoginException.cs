namespace WasteIntoCity.Core.Exceptions
{
    public class LoginException : Unauthorized401Exception
    {
        public LoginException(string? message) : base($"Error login => {message ?? "Email or password is incorrect."}")
        {
        }

        public LoginException() : base($"Error login => Email or password is incorrect.")
        {
        }
    }
}
