using StackExchange.Redis;

namespace Badminton.Web.Services
{
    public class MyService
    {
        private readonly IConnectionMultiplexer _redis;

        public MyService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void ExampleUsage()
        {
            IDatabase db = _redis.GetDatabase();
            // Use db to interact with Redis (e.g., db.StringSet, db.StringGet, etc.)
        }
    }
}