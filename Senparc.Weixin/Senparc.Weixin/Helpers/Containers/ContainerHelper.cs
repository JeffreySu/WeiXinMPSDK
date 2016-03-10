using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Helpers
{
    public class ContainerHelper
    {
        /// <summary>
        /// 获取缓存Key
        /// </summary>
        /// <returns></returns>
        public static string GetCacheKey(Type bagType)
        {
            return string.Format("Container:{0}", bagType);
        }
    }
}
