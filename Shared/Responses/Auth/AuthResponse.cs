namespace Shared.Responses.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Type { get; set; } = "Bearer";
        public int ExpiryInMinutes { get; set; }
        public string RefreshToken { get; set; }
    }
}
