namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class ValueOutOfRangeServerException<T> : InternalServer500Exception where T : notnull
    {
        protected ValueOutOfRangeServerException(string message, int code) : base(message, code)
        {
        }

        public ValueOutOfRangeServerException(string argName, string message, int code) : base($"Error: {argName} => {message}.", code)
        {
        }

        public ValueOutOfRangeServerException(string argName, T valueMin, T valueMax, int code)
            : base($"Error: {argName} => Value should be in range [{valueMin.ToString()}, {valueMax.ToString()}].", code)
        {
        }
    }
}
