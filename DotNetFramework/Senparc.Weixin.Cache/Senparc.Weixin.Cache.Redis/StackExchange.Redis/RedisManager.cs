/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：RedisManager.cs
    文件功能描述：Redis连接及数据库管理接口。

    创建标识：Senparc - 20160309

    修改标识：Senparc - 20150319
    修改描述：文件Manager.cs改名为RedisManager.cs（类名没有改动）；
              使用新的单例做法。

----------------------------------------------------------------*/

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
        #region ConnectionMultiplexer 单例

        /// <summary>
        /// _redis(ConnectionMultiplexer)单例
        /// </summary>
        internal static ConnectionMultiplexer _redis
        {
            get
            {
                return NestedRedis.instance;//返回Nested类中的静态成员instance
            }
        }

        class NestedRedis
        {
            static NestedRedis()
            {
            }
            //将instance设为一个初始化的ConnectionMultiplexer新实例
            internal static readonly ConnectionMultiplexer instance = GetManager();
        }

        #endregion

        /// <summary>
        /// 链接设置字符串
        /// </summary>
        public static string ConfigurationOption { get; set; }


        /// <summary>
        /// ConnectionMultiplexer
        /// </summary>
        public static ConnectionMultiplexer Manager
        {
            get { return _redis; }
        }

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultConnectionString()
        {
            return "localhost";
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
    }
}
