namespace ApplicationClient.Options
{
    public class UrlProjectOption
    {
        public string Url { get; set; }
        public ProjectUrlEnv Products { get; set; } = new ProjectUrlEnv();
    }
}
