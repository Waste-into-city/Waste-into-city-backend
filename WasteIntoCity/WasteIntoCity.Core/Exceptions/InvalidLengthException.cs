namespace WasteIntoCity.Core.Exceptions
{
    public class InvalidLengthException : BadRequest400Exception
    {
        protected InvalidLengthException(string message) : base(message)
        {
        }

        public InvalidLengthException(string argName, string message) : base($"Error: {argName} => {message}.")
        {
        }

        public InvalidLengthException(string argName, int valueLengthMin, int valueLengthMax) : base($"Error: {argName} => Value length should be in range [{valueLengthMin}, {valueLengthMax}].")
        {
        }
    }
}
