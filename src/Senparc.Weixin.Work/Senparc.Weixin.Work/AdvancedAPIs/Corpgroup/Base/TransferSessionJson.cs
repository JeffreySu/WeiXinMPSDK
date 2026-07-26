/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TransferSessionJson.cs
    文件功能描述：上下游企业小程序会话转换请求及返回模型

    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增上下游小程序会话转换请求与返回模型
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 获取下级或下游企业小程序会话请求。
    /// </summary>
    public class TransferSessionRequest
    {
        /// <summary>
        /// 通过上级或上游企业 code2Session 接口获取的加密成员 ID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 属于上级或上游企业的会话密钥。
        /// </summary>
        public string session_key { get; set; }
    }

    /// <summary>
    /// 获取下级或下游企业小程序会话返回结果。
    /// </summary>
    public class TransferSessionResult : WorkJsonResult
    {
        /// <summary>
        /// 下级或下游企业成员 ID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 属于下级或下游企业的会话密钥。
        /// </summary>
        public string session_key { get; set; }
    }
}
