/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：QyJsonResult.cs
    文件功能描述：企业号JSON返回结果


    创建标识：Senparc - 20150928

    修改标识：Senparc - 20160810
    修改描述：v4.1.4 QyJsonResult添加序列化标签

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 企业号 JSON 返回结果
    /// </summary>
    [Serializable]
    public class QyJsonResult : IJsonResult
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public ReturnCode_QY errcode { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 为P2P返回结果做准备
        /// </summary>
        public virtual object P2PData { get; set; }
    }
}
