namespace GreenField.Common.Configuration
{
    public class JwtOptions
    {
        public int TokenTimeLimitInMinutes { get; set; }
        public string Secret { get; set; }
    }
}