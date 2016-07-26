/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SerializerHelper.cs
    文件功能描述：unicode解码
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public class SerializerHelper
    {
        /// <summary>
        /// unicode解码
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static string DecodeUnicode(Match match)
        {
            if (!match.Success)
            {
                return null;
            }

            char outStr = (char)int.Parse(match.Value.Remove(0, 2), NumberStyles.HexNumber);
            return new string(outStr, 1);
        }

        /// <summary>
        /// 将对象转为JSON字符串
        /// </summary>
        /// <param name="data">需要生成JSON字符串的数据</param>
        /// <param name="jsonSetting">JSON输出设置</param>
        /// <returns></returns>
        public string GetJsonString(object data, JsonSetting jsonSetting = null)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.RegisterConverters(new JavaScriptConverter[]
            {
                new WeixinJsonConventer(data.GetType(), jsonSetting),
                new ExpandoJsonConverter()
            });

            var jsonString = jsSerializer.Serialize(data);

            //解码Unicode，也可以通过设置App.Config（Web.Config）设置来做，这里只是暂时弥补一下，用到的地方不多
            MatchEvaluator evaluator = new MatchEvaluator(DecodeUnicode);
            var json = Regex.Replace(jsonString, @"\\u[0123456789abcdef]{4}", evaluator);//或：[\\u007f-\\uffff]，\对应为\u000a，但一般情况下会保持\
            return json;
        }

    }

}
