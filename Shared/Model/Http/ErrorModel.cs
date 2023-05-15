namespace Shared.Model.Http
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
