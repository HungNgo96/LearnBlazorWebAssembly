namespace Application.Options
{
    public record JwtOption
    {
        public string SecurityKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string ExpiryInMinutes { get; set; }
    }
}
