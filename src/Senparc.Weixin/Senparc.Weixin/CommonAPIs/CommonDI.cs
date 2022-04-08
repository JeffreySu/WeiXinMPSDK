using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin
{
    /// <summary>
    /// 给 API 用的公共 DI 参数
    /// </summary>
    public class CommonDI
    {
        public static IServiceProvider _commonSP;
        public static IServiceProvider CommonSP
        {
            get
            {
#if !NET462
                if (_commonSP == null)
                {
                    _commonSP = Senparc.CO2NET.SenparcDI.GetServiceProvider();
                }
#endif
                return _commonSP;
            }
            set
            {
                _commonSP = value;
            }
        }
    }
}
