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
        private readonly List<string> _propertiesToIgnore;
        private readonly List<Type> _typesToIgnore;
        private readonly bool _ignoreNulls;

        public JsonSetting(bool ignoreNulls = false, List<string> propertiesToIgnore = null,
            List<Type> typesToIgnore = null)
        {
            this._ignoreNulls = ignoreNulls;
            this._propertiesToIgnore = propertiesToIgnore ?? new List<string>();
            this._typesToIgnore = typesToIgnore ?? new List<Type>();
        }
    }

    /// <summary>
    /// 微信JSON转换器
    /// </summary>
    public class WeixinJsonConventer : JavaScriptConverter
    {
        private readonly List<string> _propertiesToIgnore;
        private readonly List<Type> _typesToIgnore;
        private readonly Type _type;
        private readonly bool _ignoreNulls;

        public WeixinJsonConventer(Type type, List<string> propertiesToIgnore, List<Type> typesToIgnore, bool ignoreNulls)
        {
            this._ignoreNulls = ignoreNulls;
            this._type = type;
            this._propertiesToIgnore = propertiesToIgnore ?? new List<string>();
            this._typesToIgnore = typesToIgnore ?? new List<Type>();
        }

        public WeixinJsonConventer(Type type, bool ignoreNulls)
            : this(type, null, null, ignoreNulls)
        { }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                var typeList = new List<Type>(new[] { typeof(IJsonIgnoreNull)/*,typeof(JsonIgnoreNull)*/ });

                if (_typesToIgnore.Count > 0)
                {
                    typeList.AddRange(_typesToIgnore);
                }

                if (_ignoreNulls)
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
                if (!this._propertiesToIgnore.Contains(propertyInfo.Name))
                {
                    bool ignoreProp = propertyInfo.IsDefined(typeof(ScriptIgnoreAttribute), true);

                    if ((this._ignoreNulls || ignoreProp) && propertyInfo.GetValue(obj, null) == null)
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
