﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：StackExchangeRedisExtensions.cs
    文件功能描述：StackExchange.Redis 扩展。

    创建标识：Senparc - 20160309

    修改标识：Senparc - 20170204
    修改描述：v1.2.0 序列化方式改为 JSON

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;

#if NET461
using System.Runtime.Serialization.Formatters.Binary;
#endif

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    ///  StackExchangeRedis 扩展
    /// </summary>
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

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static byte[] Serialize(this object o)
        {
            if (o == null)
            {
                return null;
            }

#if NET461
            #region .net core后期可能会重新提供对 BinaryFormatter 的支持

            ////二进制序列化方案
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
            #endregion
#else
            //二进制序列化方案
            using (MemoryStream memoryStream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(memoryStream, o);
                byte[] objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
#endif
            //使用JSON序列化，会在Get()方法反序列化到IContainerBag的过程中出错
            //JSON序列化方案
            //SerializerHelper serializerHelper = new SerializerHelper();
            //var jsonSetting = serializerHelper.GetJsonString(o);
            //return Encoding.UTF8.GetBytes(jsonSetting);
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

#if NET461
            #region .net core后期可能会重新提供对 BinaryFormatter 的支持

            //二进制序列化方案
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }

            #endregion
#else
            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = ProtoBuf.Serializer.Deserialize<T>(memoryStream);
                return result;
            }
#endif


            //JSON序列化方案
            //SerializerHelper serializerHelper = new SerializerHelper();
            //T result = serializerHelper.GetObject<T>(Encoding.UTF8.GetString(stream));
            //return result;
        }
    }
}
