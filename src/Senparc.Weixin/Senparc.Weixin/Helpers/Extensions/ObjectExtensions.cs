using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Helpers.Extensions
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 把数据转换为Json格式（使用Newtonsoft.Json.dll）
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string ToJson(this object data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }
    }
}
