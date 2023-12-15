namespace PulseGym.Entities.Exceptions
{
    public class BadInputException : Exception
    {
        public BadInputException(string? message) : base(message) { }
    }
}
