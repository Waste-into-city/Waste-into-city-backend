namespace WasteIntoCity.Core.Exceptions
{
    public class IncorrectRequestFormatException : BadRequest400Exception
    {
        public const string DEFAULT_MESSAGE = "Request is not correct";

        private static string CreateMessage(List<string> errorMessages)
        {
            return $"{DEFAULT_MESSAGE} => {Environment.NewLine}{string.Join(Environment.NewLine, errorMessages)}.";
        }

        public IncorrectRequestFormatException(string errorsMessage) : base($"{DEFAULT_MESSAGE} => {errorsMessage}.")
        {

        }

        public IncorrectRequestFormatException(List<string> errorMessages) : base(CreateMessage(errorMessages))
        {

        }
    }
}
