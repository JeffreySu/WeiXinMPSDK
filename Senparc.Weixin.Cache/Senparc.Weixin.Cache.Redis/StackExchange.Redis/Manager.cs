using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis 链接管理
    /// </summary>
    public class RedisManager
    {
        static object _locker = new object();
        private static ConnectionMultiplexer _redis;
        /// <summary>
        /// 链接设置字符串
        /// </summary>
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

//            var redisConfigInfo = RedisConfigInfo.GetConfig();
//            #region options 设置说明

//            /*
//abortConnect ： 当为true时，当没有可用的服务器时则不会创建一个连接
//allowAdmin ： 当为true时 ，可以使用一些被认为危险的命令
//channelPrefix：所有pub/sub渠道的前缀
//connectRetry ：重试连接的次数
//connectTimeout：超时时间
//configChannel： Broadcast channel name for communicating configuration changes
//defaultDatabase ： 默认0到-1
//keepAlive ： 保存x秒的活动连接
//name:ClientName
//password:password
//proxy:代理 比如 twemproxy
//resolveDns : 指定dns解析
//serviceName ： Not currently implemented (intended for use with sentinel)
//ssl={bool} ： 使用sll加密
//sslHost={string}	： 强制服务器使用特定的ssl标识
//syncTimeout={int} ： 异步超时时间
//tiebreaker={string}：Key to use for selecting a server in an ambiguous master scenario
//version={string} ： Redis version level (useful when the server does not make this available)
//writeBuffer={int} ： 输出缓存区的大小
//    */

//            #endregion
//            var options = new ConfigurationOptions()
//            {
//                ServiceName = redisConfigInfo.ServerList,
                
//            };

            return ConnectionMultiplexer.Connect(connectionString);
        }

        private static string GetDefaultConnectionString()
        {
            return "localhost";
        }

        //private static string[] SplitString(string strSource, string split)
        //{
        //    return strSource.Split(split.ToArray());
        //}
    }
}
