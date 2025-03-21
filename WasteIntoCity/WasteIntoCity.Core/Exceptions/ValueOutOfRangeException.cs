namespace WasteIntoCity.Core.Exceptions
{
    public class ValueOutOfRangeException<T> : BadRequest400Exception where T : notnull
    {
        protected ValueOutOfRangeException(string message) : base(message)
        {
        }

        public ValueOutOfRangeException(string argName, string message) : base($"Error: {argName} => {message}.")
        {
        }

        public ValueOutOfRangeException(string argName, T valueMin, T valueMax) : base($"Error: {argName} => Value should be in range [{valueMin.ToString()}, {valueMax.ToString()}].")
        {
        }
    }
}
