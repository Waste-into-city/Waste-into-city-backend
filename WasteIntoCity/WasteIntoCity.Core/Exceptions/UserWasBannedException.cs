namespace WasteIntoCity.Core.Exceptions
{
    public class UserWasBannedException : Unauthorized401Exception
    {
        public UserWasBannedException(string? message) : base($"Error => {message ?? "User was banned."}")
        {
        }

        public UserWasBannedException() : base($"Error => User was banned.")
        {
        }
    }
}
