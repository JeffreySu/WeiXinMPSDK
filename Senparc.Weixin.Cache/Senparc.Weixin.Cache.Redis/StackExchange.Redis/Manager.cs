using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Senparc.Weixin.Cache.Redis
{
    public class RedisManager
    {
        static object _locker = new object();
        private static ConnectionMultiplexer _redis;
        public static string ConfigurationOption { get; set; }

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if (_redis == null)
                {
                    lock (_locker)
                    {
                        if (_redis != null) return _redis;
                        _redis = GetManager();
                        return _redis;
                    }
                }
                return _redis;
            }
        }
        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                if (ConfigurationOption == null)
                {
                    connectionString = GetDefaultConnectionString();
                }
                else
                {
                    return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(ConfigurationOption));
                }
            }
            return ConnectionMultiplexer.Connect(connectionString);
        }

        private static string GetDefaultConnectionString()
        {
            return "localhost";
        }
    }
}
