namespace API.GraphQL.Configuration
{
    /// <summary>
    /// Redis configurations
    /// </summary>
    public class RedisConfiguration
    {
        public bool UseInMemory { get; set; }
        public bool Ssl { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

    }
}