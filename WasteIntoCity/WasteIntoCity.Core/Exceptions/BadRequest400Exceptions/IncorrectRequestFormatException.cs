namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class IncorrectRequestFormatException : BadRequest400Exception
    {
        public const string DEFAULT_MESSAGE = "Request is not correct";

        private static string CreateMessage(List<string> errorMessages)
        {
            return $"{DEFAULT_MESSAGE} => {Environment.NewLine}{string.Join(Environment.NewLine, errorMessages)}.";
        }

        public IncorrectRequestFormatException(string errorsMessage, int code) : base($"{DEFAULT_MESSAGE} => {errorsMessage}.", code)
        {

        }

        public IncorrectRequestFormatException(List<string> errorMessages, int code) : base(CreateMessage(errorMessages), code)
        {

        }
    }
}
