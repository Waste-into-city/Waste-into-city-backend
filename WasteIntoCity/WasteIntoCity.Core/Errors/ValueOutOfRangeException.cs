namespace WasteIntoCity.Core.Errors
{
    public class ValueOutOfRangeException<T> : Exception
    {
        protected ValueOutOfRangeException(string message) : base(message)
        {
        }

        public ValueOutOfRangeException(string argName, string message) : base($"{argName}: {message}.")
        {
        }

        public ValueOutOfRangeException(string argName, T valueMin, T valueMax) : base($"{argName}: Value should be in range [{valueMin}, {valueMax}].")
        {
        }
    }
}
