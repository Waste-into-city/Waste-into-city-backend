namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class InvalidLengthException : BadRequest400Exception
    {
        protected InvalidLengthException(string message, int code) : base(message, code)
        {
        }

        public InvalidLengthException(string argName, string message, int code) : base($"Error: {argName} => {message}.", code)
        {
        }

        public InvalidLengthException(int code, string argName, int valueLengthMin, int valueLengthMax)
            : base($"Error: {argName} => Value length should be in range [{valueLengthMin}, {valueLengthMax}].", code)
        {
        }
    }
}
