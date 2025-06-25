namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class WhiteSpacesOrInvalidLengthException : InvalidLengthException
    {
        protected WhiteSpacesOrInvalidLengthException(string message, int code) : base(message, code)
        {
        }

        public WhiteSpacesOrInvalidLengthException(string argName, string message, int code) : base(argName, message, code)
        {
        }

        public WhiteSpacesOrInvalidLengthException(string argName, int valueLengthMin, int valueLengthMax, int code)
            : base($"Error: {argName} => Value cannot be only with white spaces and length should be in range [{valueLengthMin}," +
                  $" {valueLengthMax}].", code)
        {
        }
    }
}
