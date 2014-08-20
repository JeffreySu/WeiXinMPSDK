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

        public static T DeserializeJson<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        } 
        #endregion
}
