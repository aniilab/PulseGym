namespace PulseGym.Entities.DTO
{
    public class TokensDTO
    {
        public required string AccessToken { get; init; }

        public required string RefreshToken { get; init; }
    }
}
