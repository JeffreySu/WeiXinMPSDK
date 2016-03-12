using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// RedisUtils
    /// </summary>
    public static class RedisUtils
    {
        /// <summary>
        /// Serialize in Redis format
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HashEntry[] ToHashEntries(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            return properties
                .Where(x => x.GetIndexParameters().Length == 0 && x.GetValue(obj) != null) // <-- PREVENT NullReferenceException
                .Select(property => new HashEntry(property.Name, property.GetValue(obj)
                .ToString())).ToArray();
        }

        /// <summary>
        /// Deserialize from Redis format
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashEntries"></param>
        /// <returns></returns>
        public static T ConvertFromRedis<T>(this HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) continue;
                property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
            }
            return (T)obj;
        }
    }
}
