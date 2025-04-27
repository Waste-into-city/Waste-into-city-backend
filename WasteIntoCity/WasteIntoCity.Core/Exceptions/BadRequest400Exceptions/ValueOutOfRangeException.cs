namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class ValueOutOfRangeException<T> : BadRequest400Exception where T : notnull
    {
        protected ValueOutOfRangeException(string message, int code) : base(message, code)
        {
        }

        public ValueOutOfRangeException(string argName, string message, int code) : base($"Error: {argName} => {message}.", code)
        {
        }

        public ValueOutOfRangeException(string argName, T valueMin, T valueMax, int code)
            : base($"Error: {argName} => Value should be in range [{valueMin.ToString()}, {valueMax.ToString()}].", code)
        {
        }
    }
}
