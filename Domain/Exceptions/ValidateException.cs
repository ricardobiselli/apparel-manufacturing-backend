namespace Domain.Exceptions
{
    public class ValidateException : Exception
    {
        public ValidateException(string message) : base(message)
        {
        }

        public ValidateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
