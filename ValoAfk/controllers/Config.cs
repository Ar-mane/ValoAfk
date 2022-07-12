namespace ValoAfk.controllers
{
    public class Config
    {
        public string Pattern { get; set; }
        public int Timeout { get; set; }
        public bool RememberMe { get; set; }


        public Config(string pattern, int timeOut, bool rememberMe)
        {
            (Pattern, Timeout, RememberMe) = (pattern, timeOut, rememberMe);
        }
    }
}