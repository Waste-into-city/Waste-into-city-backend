namespace WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions
{
    public class UserWasBannedException : Unauthorized401Exception
    {
        public UserWasBannedException(string? message, int code) : base($"Error => {message ?? "User was banned."}", code)
        {
        }

        public UserWasBannedException(int code) : base($"Error => User was banned.", code)
        {
        }
    }
}
