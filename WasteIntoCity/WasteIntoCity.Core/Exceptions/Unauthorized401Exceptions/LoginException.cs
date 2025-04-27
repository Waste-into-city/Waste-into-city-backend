namespace WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions
{
    public class LoginException : Unauthorized401Exception
    {
        public LoginException(string? message, int code) : base($"Error login => {message ?? "Email or password is incorrect."}", code)
        {
        }

        public LoginException(int code) : base($"Error login => Email or password is incorrect.", code)
        {
        }
    }
}
