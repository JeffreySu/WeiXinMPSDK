using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 所有 JSON 格式返回值的API返回结果接口
    /// </summary>
    public interface IJsonResult// : IJsonResultCallback
    {
        /// <summary>
        /// 返回结果信息
        /// </summary>
        string errmsg { get; set; }
        object P2PData { get; set; }
    }

    /// <summary>
    /// 包含 errorcode 的 Json 返回结果接口
    /// </summary>
    public interface IWxJsonResult : IJsonResult
    {
        /// <summary>
        /// 返回结果代码
        /// </summary>
        ReturnCode errcode { get; set; }
    }
}
