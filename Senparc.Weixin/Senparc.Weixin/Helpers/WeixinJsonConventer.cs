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
    /// 微信JSON转换器
    /// </summary>
    public class WeixinJsonConventer : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            //这里为了尽量“避免”使用JSON.NET，我们自己加一个过滤null值的转换器（当然JSON.NET也是不错的选择，只不过这里我们尽可能将SDK保持得更加“轻”一些，并且降低对外部的依赖，提高兼容性）

            //throw new InvalidOperationException("object must be of the EntityBase type");


            //方案一
            var jsonExample = new Dictionary<string, object>();
       
            foreach (var prop in obj.GetType().GetProperties())
            {
                //check if decorated with ScriptIgnore attribute
                bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

                var value = prop.GetValue(obj, BindingFlags.Public, null, null, null);

                if (value != null && !ignoreProp)
                {
                    jsonExample.Add(prop.Name, value);
                }
                else
                {
                    jsonExample.Add(prop.Name, "[]");
                    //return null;
                }
            }

            return jsonExample;

            //方案二
            //var toSerialize = new Dictionary<string, object>();

            //foreach (var prop in obj.GetType()
            //                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //                        .Select(p => new
            //                        {
            //                            Name = p.Name,
            //                            Value = p.GetValue(obj)
            //                        })
            //                        .Where(p => p.Value != null))
            //{
            //    toSerialize.Add(prop.Name, prop.Value);
            //}

            //return toSerialize;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return GetType().Assembly.GetTypes(); }
            //get
            //{
            //    return new ReadOnlyCollection<Type>(new Type[] { typeof(EntityBase) });
            //}
        }
    }
}
