using StackExchange.Redis;
using JB.models;

namespace JB.services
{
    public class RedisService 
    {
        private ConnectionMultiplexer redis;
        private IDatabase db;
        public RedisService(IRedisSettings redisSettings)
        {
             redis = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
             db = redis.GetDatabase();
        }

        public bool HasKey(string key)
        {
          return db.KeyExists(key);
        }

        public string ReadKey(string key)
        {
          return db.StringGet(key);
        }

        public bool WriteKey(string key, string value)
        {
          return db.StringSet(key, value);
        }

    }
}