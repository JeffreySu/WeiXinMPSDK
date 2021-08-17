using Senparc.CO2NET.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// HTTP 请求时 Header 中 User-Agent 的参数集合
    /// </summary>
    public class UserAgentValues
    {
        #region 单例

        /// <summary>
        /// UserAgentValues 的构造函数
        /// </summary>
        UserAgentValues() : base()
        {
        }

        public static UserAgentValues Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly UserAgentValues instance = new UserAgentValues();
        }

        #endregion

        /// <summary>
        /// 当前模块版本号
        /// </summary>
        public string SenparcWeixinVersion = typeof(Senparc.Weixin.Config).Assembly.GetName().Version.ToString();
        /// <summary>
        /// 当前模块版本
        /// </summary>
        public string TenPayV3Version = typeof(TenPayApiRequest).Assembly.GetName().Version.ToString();
        /// <summary>
        /// 操作系统名称及版本
        /// </summary>
        public string OSVersion = Environment.OSVersion.ToString();
        /// <summary>
        /// 当前 .NET 运行时版本
        /// </summary>
        public string RuntimeVersion = Environment.Version.ToString();
    }
}
