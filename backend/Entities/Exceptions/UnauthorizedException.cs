namespace PulseGym.Entities.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message) : base(message) { }
    }
}
