   namespace Senparc.Weixin.MP.Helpers
{
   
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    public static class exmethod
    {
   #region Json序列化和反序列化扩展方法
        //http://james.newtonking.com/json/help/index.html?topic=html/JsonNetVsDotNetSerializers.htm
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter());
        }

         /// <summary>
        /// 反序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        } 
        
                /// <summary>
        /// 获取Json string某节点的值。
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetJosnValue(this string jsonStr, string key)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(jsonStr))
            {
                key = "\"" + key.Trim('"') + "\"";
                int index = jsonStr.IndexOf(key) + key.Length + 1;
                if (index > key.Length + 1)
                {
                    //先截逗号，若是最后一个，截“｝”号，取最小值
                    int end = jsonStr.IndexOf(',', index);
                    if (end == -1)
                    {
                        end = jsonStr.IndexOf('}', index);
                    }
                    //index = json.IndexOf('"', index + key.Length + 1) + 1;
                    result = jsonStr.Substring(index, end - index);
                    //过滤引号或空格
                    result = result.Trim(new char[] { '"', ' ', '\'' });
                }
            }
            return result;
        }
        
        #endregion
}
