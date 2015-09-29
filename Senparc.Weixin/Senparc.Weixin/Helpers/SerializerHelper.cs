/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：SerializerHelper.cs
    文件功能描述：unicode解码
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Helpers
{
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

            char outStr = (char)int.Parse(match.Value.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            return new string(outStr, 1);
        }

        /// <summary>
        /// 将对象转为JSON字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ignoreNulls">是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中</param>
        /// <param name="propertiesToIgnore">需要特殊忽略null值的属性类型</param>
        /// <returns></returns>
        public string GetJsonString(object data, bool ignoreNulls = false, List<Type> propertiesToIgnore = null)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.RegisterConverters(new[] { new PropertyExclusionConverter(data.GetType(), true) });

            var jsonString = jsSerializer.Serialize(data);

            //解码Unicode，也可以通过设置App.Config（Web.Config）设置来做，这里只是暂时弥补一下，用到的地方不多
            MatchEvaluator evaluator = new MatchEvaluator(DecodeUnicode);
            var json = Regex.Replace(jsonString, @"\\u[0123456789abcdef]{4}", evaluator);//或：[\\u007f-\\uffff]，\对应为\u000a，但一般情况下会保持\
            return json;
        }


        private class NullPropertiesConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                var jsonExample = new Dictionary<string, object>();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    //check if decorated with ScriptIgnore attribute
                    bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

                    var value = prop.GetValue(obj, BindingFlags.Public, null, null, null);
                    if (value != null && !ignoreProp)
                        jsonExample.Add(prop.Name, value);
                }

                return jsonExample;
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return GetType().Assembly.GetTypes(); }
            }
        }
    }

    public class PropertyExclusionConverter : JavaScriptConverter
    {
        private readonly List<string> propertiesToIgnore;
        private readonly Type type;
        private readonly bool ignoreNulls;

        public PropertyExclusionConverter(Type type, List<string> propertiesToIgnore, bool ignoreNulls)
        {
            this.ignoreNulls = ignoreNulls;
            this.type = type;
            this.propertiesToIgnore = propertiesToIgnore ?? new List<string>();
        }

        public PropertyExclusionConverter(Type type, bool ignoreNulls)
            : this(type, null, ignoreNulls)
        { }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                var typeList = new List<Type>(new[] { typeof(IJsonIgnoreNull) });
                if (ignoreNulls)
                {
                    typeList.Add(type);
                }

                return new ReadOnlyCollection<Type>(typeList);
            }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var result = new Dictionary<string, object>();
            if (obj == null)
            {
                return result;
            }
            var properties = obj.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (!this.propertiesToIgnore.Contains(propertyInfo.Name))
                {
                    if (this.ignoreNulls && propertyInfo.GetValue(obj, null) == null)
                    {
                        continue;
                    }

                    result.Add(propertyInfo.Name, propertyInfo.GetValue(obj, null));
                }
            }
            return result;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException(); //Converter is currently only used for ignoring properties on serialization
        }
    }
}
