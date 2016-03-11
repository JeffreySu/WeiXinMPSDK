using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache.Redis
{
    public static class StackExchangeRedisExtensions
    {

        //public static T Get<T>(string key)
        //{
        //    var connect = AzureredisDb.Cache;
        //    var r = AzureredisDb.Cache.StringGet(key);
        //    return Deserialize<T>(r);
        //}

        //public static List<T> GetList<T>(string key)
        //{
        //    return (List<T>)Get(key);
        //}

        //public static void SetList<T>(string key, List<T> list)
        //{
        //    Set(key, list);
        //}

        //public static object Get(string key)
        //{
        //    return Deserialize<object>(AzureredisDb.Cache.StringGet(key));
        //}

        //public static void Set(string key, object value)
        //{
        //    AzureredisDb.Cache.StringSet(key, Serialize(value));
        //}

        public static byte[] Serialize(this object o)
        {
            if (o == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        public static T Deserialize<T>(this byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }

}
