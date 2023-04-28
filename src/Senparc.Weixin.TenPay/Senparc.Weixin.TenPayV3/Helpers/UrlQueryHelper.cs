using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    internal class UrlQueryHelper
    {
        /// <summary>
        /// 对象转换为 url 参数 忽略空数据以及嵌套对象
        /// object => key1=value1&key2=value2...
        /// </summary>
        /// <param name="this"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ToParams(object @this, string prefix = "?")
        {
            if (@this == null)
                return string.Empty;

            var jsonObject = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(@this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }));

            var kvs = new List<string>();
            foreach (var item in jsonObject)
            {
                if (item.Value == null)
                    continue;

                if (item.Value.Type == JTokenType.Object || item.Value.Type == JTokenType.Array)
                    continue;

                kvs.Add($"{item.Key}={item.Value}");
            }
            if (kvs.Count <= 0)
                return string.Empty;

            return $"{prefix}{string.Join("&", kvs)}";
        }
    }
}
