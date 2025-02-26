namespace WasteIntoCity.Core.Errors
{
    public class WhiteSpacesOrInvalidLengthException : InvalidLengthException
    {
        protected WhiteSpacesOrInvalidLengthException(string message) : base(message)
        {
        }

        public WhiteSpacesOrInvalidLengthException(string argName, string message) : base(argName, message)
        {
        }

        public WhiteSpacesOrInvalidLengthException(string argName, int valueLengthMin, int valueLengthMax) : base($"{argName}: Value cannot be only with white spaces and length should be in range [{valueLengthMin}, {valueLengthMax}].")
        {
        }
    }
}
