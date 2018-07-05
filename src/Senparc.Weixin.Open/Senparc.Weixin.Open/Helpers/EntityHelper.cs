using System.Xml.Linq;
//using Senparc.Weixin.Helpers;
using System.Reflection;
using Senparc.CO2NET.Utilities;

namespace Senparc.Weixin.Open.Helpers
{
    public static class EntityHelper
    {
        public static void FillEntityWithXml<T>(this T entity, XDocument doc) where T : class, new()
        {
            entity = entity ?? new T();
            var root = doc.Root;

            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (root.Element(propName) != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        //case "String":
                        //    goto default;
                        case "DateTime":
                        case "Int32":
                        case "Int64":
                        case "Double":
                        case "Nullable`1": //可为空对象
                            EntityUtility.FillSystemType(entity, prop, root.Element(propName).Value);
                            break;
                        case "Boolean":
                            if (propName == "FuncFlag")
                            {
                                EntityUtility.FillSystemType(entity, prop, root.Element(propName).Value == "1");
                            }
                            else
                            {
                                goto default;
                            }
                            break;

                        //以下为枚举类型
                        case "RequestInfoType":
                            //已设为只读
                            break;
                        default:
                            prop.SetValue(entity, root.Element(propName).Value, null);
                            break;
                    }
                }
            }
        }

    }
}
