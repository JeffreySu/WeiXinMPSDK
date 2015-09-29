using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        /// 是否忽略Null值的属性
        /// </summary>
        public bool IgnoreNulls { get; set; }
        /// <summary>
        /// 需要忽略的属性名称
        /// </summary>
        public List<string> PropertiesToIgnore { get; set; }
        /// <summary>
        /// 包含需要忽略的属性的类的类型
        /// </summary>
        public List<Type> TypesToIgnore { get; set; }


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
