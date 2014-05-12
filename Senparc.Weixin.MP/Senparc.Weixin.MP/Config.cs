using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP
{
    public static class Config
    {
        /// <summary>
        /// 请求超时设置（以毫秒为单位）
        /// </summary>
        private static int _timeOut = 10000;

        public static int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }
    }
}
