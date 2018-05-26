#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：WeixinJsonConventer.cs
    文件功能描述：微信JSON字符串转换
    
    
    创建标识：Senparc - 20150930
    
    修改标识：Senparc - 20160722
    修改描述：增加特性，对json格式的输出内容的控制，对枚举类型字符串输出、默认值不输出、例外属性等，如会员卡卡里面的CodeType
             IDictionary中foreach中的内容的修改

    修改标识：Senparc - 20160722
    修改描述：v4.11.5 修复WeixinJsonConventer.Serialize中的错误。感谢 @jiehanlin
    
    修改标识：Senparc - 20180526
    修改描述：v4.22.0-rc1 将 JsonSetting 继承 JsonSerializerSettings，使用 Newtonsoft.Json 进行序列化
    
----------------------------------------------------------------*/



using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
//#if NET35 || NET40 || NET45
//using System.Web.Script.Serialization;
//#endif

using Senparc.Weixin.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// JSON输出设置
    /// </summary>
    public class JsonSetting : JsonSerializerSettings
    {
        /// <summary>
        /// 是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中
        /// </summary>
        public bool IgnoreNulls { get; set; }
        /// <summary>
        /// 需要特殊忽略null值的属性名称
        /// </summary>
        public List<string> PropertiesToIgnoreNull { get; set; }
        /// <summary>
        /// 指定类型（Class，非Interface）下的为null属性不生成到Json中
        /// </summary>
        public List<Type> TypesToIgnoreNull { get; set; }

        #region Add


        public class IgnoreValueAttribute : System.ComponentModel.DefaultValueAttribute
        {
            public IgnoreValueAttribute(object value) : base(value)
            {
                //Value = value;
            }
        }
        public class IgnoreNullAttribute : Attribute
        {

        }
        /// <summary>
        /// 例外属性，即不排除的属性值
        /// </summary>
        public class ExcludedAttribute : Attribute
        {

        }

        /// <summary>
        /// 枚举类型显示字符串
        /// </summary>
        public class EnumStringAttribute : Attribute
        {

        }

        #endregion
        /// <summary>
        /// JSON 输出设置 构造函数
        /// </summary>
        /// <param name="ignoreNulls">是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中</param>
        /// <param name="propertiesToIgnoreNull">需要特殊忽略null值的属性名称</param>
        /// <param name="typesToIgnoreNull">指定类型（Class，非Interface）下的为null属性不生成到Json中</param>
        public JsonSetting(bool ignoreNulls = false, List<string> propertiesToIgnoreNull = null, List<Type> typesToIgnoreNull = null)
        {
            IgnoreNulls = ignoreNulls;
            PropertiesToIgnoreNull = propertiesToIgnoreNull ?? new List<string>();
            TypesToIgnoreNull = typesToIgnoreNull ?? new List<Type>();
        }
    }

#if NET35 || NET40 || NET45

//    /// <summary>
//    /// 微信 JSON 转换器
//    /// </summary>
//    public class WeixinJsonConventer : JavaScriptConverter
//    {
//        private readonly JsonSetting _jsonSetting;
//        private readonly Type _type;

//        public WeixinJsonConventer(Type type, JsonSetting jsonSetting = null)
//        {
//            this._jsonSetting = jsonSetting ?? new JsonSetting();
//            this._type = type;
//        }

//        public override IEnumerable<Type> SupportedTypes
//        {
//            get
//            {
//                var typeList = new List<Type>(new[] { typeof(IJsonIgnoreNull), typeof(IJsonEnumString)/*,typeof(JsonIgnoreNull)*/ });

//                if (_jsonSetting.TypesToIgnoreNull.Count > 0)
//                {
//                    typeList.AddRange(_jsonSetting.TypesToIgnoreNull);
//                }

//                if (_jsonSetting.IgnoreNulls)
//                {
//                    typeList.Add(_type);
//                }

//                return new ReadOnlyCollection<Type>(typeList);
//            }
//        }

//        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
//        {
//            var result = new Dictionary<string, object>();
//            if (obj == null)
//            {
//                return result;
//            }

//            var properties = obj.GetType().GetProperties();
//            foreach (var propertyInfo in properties)
//            {
//                //continue;
//                //排除的属性
//                bool excludedProp = propertyInfo.IsDefined(typeof(JsonSetting.ExcludedAttribute), true);
//                if (excludedProp)
//                {
//                    result.Add(propertyInfo.Name, propertyInfo.GetValue(obj, null));
//                }
//                else
//                {
//                    if (!this._jsonSetting.PropertiesToIgnoreNull.Contains(propertyInfo.Name))
//                    {
//                        bool ignoreProp = propertyInfo.IsDefined(typeof(ScriptIgnoreAttribute), true);
//                        if ((this._jsonSetting.IgnoreNulls || ignoreProp) && propertyInfo.GetValue(obj, null) == null)
//                        {
//                            continue;
//                        }


//                        //当值匹配时需要忽略的属性

//#if NET35 || NET40
//                        JsonSetting.IgnoreValueAttribute attri = propertyInfo.GetCustomAttributes(typeof(JsonSetting.IgnoreValueAttribute), false).FirstOrDefault() as JsonSetting.IgnoreValueAttribute;
//                        if (attri != null && attri.Value.Equals(propertyInfo.GetValue(obj, null)))
//                        {
//                            continue;
//                        }

//                        JsonSetting.EnumStringAttribute enumStringAttri = propertyInfo.GetCustomAttributes(typeof(JsonSetting.EnumStringAttribute), false).FirstOrDefault() as JsonSetting.EnumStringAttribute;
//                        if (enumStringAttri != null)
//                        {
//                            //枚举类型显示字符串
//                            result.Add(propertyInfo.Name, propertyInfo.GetValue(obj, null).ToString());
//                        }
//                        else
//                        {
//                            result.Add(propertyInfo.Name, propertyInfo.GetValue(obj, null));
//                        }
//#else
//                        JsonSetting.IgnoreValueAttribute attri = propertyInfo.GetCustomAttribute<JsonSetting.IgnoreValueAttribute>();
//                        if (attri != null && attri.Value.Equals(propertyInfo.GetValue(obj)))
//                        {
//                            continue;
//                        }

//                        JsonSetting.EnumStringAttribute enumStringAttri = propertyInfo.GetCustomAttribute<JsonSetting.EnumStringAttribute>();
//                        if (enumStringAttri != null)
//                        {
//                            //枚举类型显示字符串
//                            result.Add(propertyInfo.Name, propertyInfo.GetValue(obj).ToString());
//                        }
//                        else
//                        {
//                            result.Add(propertyInfo.Name, propertyInfo.GetValue(obj, null));
//                        }
//#endif
//                    }
//                }
//            }
//            return result;
//        }

//        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
//        {
//            throw new NotImplementedException(); //Converter is currently only used for ignoring properties on serialization
//        }
//    }

#if NET35||NET40||NET45
    public class WeiXinJsonSetting : JsonSerializerSettings
    {
        public WeiXinJsonSetting() { }

        public WeiXinJsonSetting(JsonSetting jsonSetting)
        {
            if (jsonSetting == null)
            {
                jsonSetting = new JsonSetting();
            }
            ContractResolver = new WeiXinJsonContractResolver(jsonSetting.IgnoreNulls, jsonSetting.PropertiesToIgnoreNull, jsonSetting.TypesToIgnoreNull);
        }
        /// <summary>
        /// JSON 输出设置 构造函数  优先级： ignoreNulls < propertiesToIgnoreNull < typesToIgnoreNull
        /// </summary>
        /// <param name="ignoreNulls">是否忽略具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中</param>
        /// <param name="propertiesToIgnoreNull">需要特殊忽略null值的属性名称</param>
        /// <param name="typesToIgnoreNull">指定类型（Class，非Interface）下的为null属性不生成到Json中</param>
        public WeiXinJsonSetting(bool ignoreNulls = false, List<string> propertiesToIgnoreNull = null, List<Type> typesToIgnoreNull = null)
        {
            ContractResolver = new WeiXinJsonContractResolver(ignoreNulls, propertiesToIgnoreNull, typesToIgnoreNull);
        }

    }
    public class WeiXinJsonContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中
        /// </summary>
        bool IgnoreNulls;
        /// <summary>
        /// 需要特殊忽略null值的属性名称
        /// </summary>
        public List<string> PropertiesToIgnoreNull { get; set; }
        /// <summary>
        /// 指定类型（Class，非Interface）下的为null属性不生成到Json中
        /// </summary>
        public List<Type> TypesToIgnoreNull { get; set; }
        /// <summary>
        /// JSON 输出设置 构造函数  优先级： ignoreNulls &lt; propertiesToIgnoreNull &lt; typesToIgnoreNull
        /// </summary>
        /// <param name="ignoreNulls">是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中</param>
        /// <param name="propertiesToIgnoreNull">需要特殊忽略null值的属性名称</param>
        /// <param name="typesToIgnoreNull">指定类型（Class，非Interface）下的为null属性不生成到Json中</param>
        public WeiXinJsonContractResolver(bool ignoreNulls = false, List<string> propertiesToIgnoreNull = null, List<Type> typesToIgnoreNull = null)
        {
            IgnoreNulls = ignoreNulls;
            PropertiesToIgnoreNull = propertiesToIgnoreNull;
            TypesToIgnoreNull = typesToIgnoreNull;
        }
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            //TypesToIgnoreNull指定类型（Class，非Interface）下的为null属性不生成到Json中
            if (TypesToIgnoreNull.Contains(type))
            {
                type.IsDefined(typeof(JsonSetting.IgnoreNullAttribute), false);
            }
            return base.CreateProperties(type, memberSerialization);
        }
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

#if NET45
            //IgnoreNull标注的字段根据IgnoreNulls设定是否序列化
            var ignoreNull = member.GetCustomAttribute<JsonSetting.IgnoreNullAttribute>();
            if (ignoreNull != null || IgnoreNulls)
            {
                property.NullValueHandling = NullValueHandling.Ignore;
            }
            else
            {
                property.NullValueHandling = NullValueHandling.Include;
            }

            //propertiesToIgnoreNull指定字段为Null时不序列化
            if (PropertiesToIgnoreNull.Contains(member.Name))
            {
                property.NullValueHandling = NullValueHandling.Ignore;
            }

            ////符合IgnoreValue标注值的字段不序列化
            //var ignoreValue = member.GetCustomAttribute<JsonSetting.IgnoreValueAttribute>();
            //if (ignoreValue != null)
            //{
            //    property.DefaultValueHandling = DefaultValueHandling.Ignore;
            //    var t = member.DeclaringType;
            //    property.ShouldSerialize = instance =>
            //    {
            //        var obj = Convert.ChangeType(instance, t);
            //        var value = (member as PropertyInfo).GetValue(obj, null);
            //        return value != ignoreValue.Value;
            //    };
            //}

            //枚举序列化
            var enumString = member.GetCustomAttribute<JsonSetting.EnumStringAttribute>();
            if (enumString != null)
            {
                property.Converter = new StringEnumConverter();
                //property = base.CreateProperty(member, memberSerialization);
            }
#else
            var customAttributes = member.GetCustomAttributes(false);
            var ignoreNullAttribute = typeof(JsonSetting.IgnoreNullAttribute);
            //IgnoreNull标注的字段根据IgnoreNulls设定是否序列化
            if (IgnoreNulls || customAttributes.Count(o => o.GetType() == ignoreNullAttribute) == 1)
            {
                property.NullValueHandling = NullValueHandling.Ignore;
            }
            else
            {
                property.NullValueHandling = NullValueHandling.Include;
            }

            //propertiesToIgnoreNull指定字段为Null时不序列化
            if (PropertiesToIgnoreNull.Contains(member.Name))
            {
                property.NullValueHandling = NullValueHandling.Ignore;
            }

            ////符合IgnoreValue标注值的字段不序列化
            //var ignoreValueAttribute = typeof(JsonSetting.IgnoreValueAttribute);
            //var ignoreValue = customAttributes.FirstOrDefault(o => o.GetType() == ignoreValueAttribute);
            //if (ignoreValue != null)
            //{
            //    property.DefaultValueHandling = DefaultValueHandling.Ignore;
            //    var t = member.DeclaringType;
            //    property.ShouldSerialize = instance =>
            //    {
            //        var obj = Convert.ChangeType(instance, t);
            //        var value = (member as PropertyInfo).GetValue(obj, null);
            //        return value != (ignoreValue as JsonSetting.IgnoreValueAttribute).Value;
            //    };
            //}

            //枚举序列化
            var enumStringAttribute = typeof(JsonSetting.EnumStringAttribute);
            if (customAttributes.Count(o => o.GetType() == enumStringAttribute) == 1)
            {
                property.Converter = new StringEnumConverter();
            }
#endif

            //var defaultIgnore = member.GetCustomAttribute<DefaultIgnoreAttribute>();
            //if (defaultIgnore != null)
            //{
            //    //defaultIgnore.Value == member.
            //}
            return property;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            return base.CreateContract(objectType);
        }
    }
#endif

#endif

}
