using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.NeuChar
{
    public enum DataType
    {
        List,
        Unique
    }

    public enum ApiType
    {
        AccessToken,
        Normal
    }

    /// <summary>
    /// NeuChar 消息的乐行
    /// </summary>
    public enum NeuCharMessageType
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        GetConfig,
        /// <summary>
        /// 储存配置
        /// </summary>
        SaveConfig,
    }
}
