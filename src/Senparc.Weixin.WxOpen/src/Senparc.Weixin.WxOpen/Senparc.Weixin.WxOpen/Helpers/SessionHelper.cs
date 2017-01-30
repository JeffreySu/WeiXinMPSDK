/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：SessionHelper.cs
    文件功能描述：Session帮助类
    
    
    创建标识：Senparc - 20170129
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Helpers
{
    /// <summary>
    /// SessionHelper
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// 获取新的3rdSession名称
        /// </summary>
        /// <param name="bSize">Session名称长度，单位：B，建议为16的倍数，通常情况下16B已经够用（32位GUID字符串）</param>
        /// <returns></returns>
        public static string GetNewThirdSessionName(int bSize = 16)
        {
            string key = null;
            for (int i = 0; i < bSize / 16; i++)
            {
                key += Guid.NewGuid().ToString("n");
            }
            return key;
        }
    }
}
