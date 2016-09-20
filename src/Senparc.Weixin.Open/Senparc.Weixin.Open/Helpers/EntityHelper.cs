using System.Xml.Linq;
using Senparc.Weixin.Helpers;

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
                            prop.SetValue(entity, DateTimeHelper.GetDateTimeFromXml(root.Element(propName).Value), null);
                            break;
                        case "Boolean":
                            if (propName == "FuncFlag")
                            {
                                prop.SetValue(entity, root.Element(propName).Value == "1", null);
                            }
                            else
                            {
                                goto default;
                            }
                            break;
                        case "Int32":
                            prop.SetValue(entity, int.Parse(root.Element(propName).Value), null);
                            break;
                        case "Int64":
                            prop.SetValue(entity, long.Parse(root.Element(propName).Value), null);
                            break;
                        case "Double":
                            prop.SetValue(entity, double.Parse(root.Element(propName).Value), null);
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
