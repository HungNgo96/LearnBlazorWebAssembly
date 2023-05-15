namespace Shared.Model.Http
{
    public class HttpOption
    {
        public HttpClient Client { get; set; }
        public string? BaseAddress { get; set; }
        public int Timeout { get; set; } = 2;
        public string? Token { get; set; }

        public string AuthType { get; set; } = "Bearer";
    }
}
