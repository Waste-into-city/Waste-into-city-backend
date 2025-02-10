namespace WasteIntoCity.Core.Errors
{
    public class InvalidLengthException : ValueOutOfRangeException<int>
    {
        protected InvalidLengthException(string message) : base(message)
        {
        }

        public InvalidLengthException(string argName, string message) : base($"{argName}: {message}.")
        {
        }

        public InvalidLengthException(string argName, int valueLengthMin, int valueLengthMax) : base($"{argName}: Value length should be in range [{valueLengthMin}, {valueLengthMax}].")
        {
        }
    }
}
