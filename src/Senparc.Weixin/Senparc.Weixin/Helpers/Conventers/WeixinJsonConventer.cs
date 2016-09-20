/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinJsonConventer.cs
    文件功能描述：微信JSON字符串转换
    
    
    创建标识：Senparc - 20150930
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// JSON输出设置
    /// </summary>
    public class JsonSetting
    {
        /// <summary>
        /// 是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中
        /// </summary>
        public bool IgnoreNulls { get; set; }
        /// <summary>
        /// 需要特殊忽略null值的属性名称
        /// </summary>
        public List<string> PropertiesToIgnore { get; set; }
        /// <summary>
        /// 指定类型（Class，非Interface）下的为null属性不生成到Json中
        /// </summary>
        public List<Type> TypesToIgnore { get; set; }


        /// <summary>
        /// JSON输出设置 构造函数
        /// </summary>
        /// <param name="ignoreNulls">是否忽略当前类型以及具有IJsonIgnoreNull接口，且为Null值的属性。如果为true，符合此条件的属性将不会出现在Json字符串中</param>
        /// <param name="propertiesToIgnore">需要特殊忽略null值的属性名称</param>
        /// <param name="typesToIgnore">指定类型（Class，非Interface）下的为null属性不生成到Json中</param>
        public JsonSetting(bool ignoreNulls = false, List<string> propertiesToIgnore = null, List<Type> typesToIgnore = null)
        {
            IgnoreNulls = ignoreNulls;
            PropertiesToIgnore = propertiesToIgnore ?? new List<string>();
            TypesToIgnore = typesToIgnore ?? new List<Type>();
        }
    }

    /// <summary>
    /// 微信JSON转换器
    /// </summary>
    public class WeixinJsonConventer : JavaScriptConverter
    {
        private readonly JsonSetting _jsonSetting;
        private readonly Type _type;

        public WeixinJsonConventer(Type type, JsonSetting jsonSetting = null)
        {
            this._jsonSetting = jsonSetting ?? new JsonSetting();
            this._type = type;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                var typeList = new List<Type>(new[] { typeof(IJsonIgnoreNull)/*,typeof(JsonIgnoreNull)*/ });

                if (_jsonSetting.TypesToIgnore.Count > 0)
                {
                    typeList.AddRange(_jsonSetting.TypesToIgnore);
                }

                if (_jsonSetting.IgnoreNulls)
                {
                    typeList.Add(_type);
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
                if (!this._jsonSetting.PropertiesToIgnore.Contains(propertyInfo.Name))
                {
                    bool ignoreProp = propertyInfo.IsDefined(typeof(ScriptIgnoreAttribute), true);

                    if ((this._jsonSetting.IgnoreNulls || ignoreProp) && propertyInfo.GetValue(obj, null) == null)
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
