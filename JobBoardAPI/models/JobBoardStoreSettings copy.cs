namespace JB.models
{
    public class RedisSettings : IRedisSettings
    {
        public string ConnectionString { get; set; }
    }

    public interface IRedisSettings
    {
        string ConnectionString { get; set; }
    }
}    