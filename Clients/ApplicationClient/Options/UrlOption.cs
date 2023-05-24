namespace ApplicationClient.Options
{
    public class UrlOption
    {
        public string Url { get; set; }
        public ProductOption Products { get; set; } = new ProductOption();
        public AccountOption Accounts { get; set; } = new AccountOption();
        public TokenOption Token { get; set; } = new TokenOption();
        public ChartOption Charts { get; set; } = new ChartOption();
    }
}
