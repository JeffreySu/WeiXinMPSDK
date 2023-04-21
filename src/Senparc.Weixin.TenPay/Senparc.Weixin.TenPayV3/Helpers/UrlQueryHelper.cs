using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    internal class UrlQueryHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ToParams(object @this, string prefix = "?")
        {
            var param = new StringBuilder();
            if (@this == null)
                return param.ToString();

            var serializeSetting = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var jObj = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(@this, serializeSetting));
            foreach (var item in jObj)
            {
                if (item.Value == null)
                    continue;

                if (item.Value.Type == JTokenType.Object || item.Value.Type == JTokenType.Array)
                    continue;

                param.Append($"{item.Key}={item.Value}&");
            }
            return $"{prefix}{param.ToString().Trim(new char[] { '&' })}";
        }
    }
}
