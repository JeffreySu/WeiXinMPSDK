/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：QyJsonResult.cs
    文件功能描述：企业号JSON返回结果


    创建标识：Senparc - 20150928
----------------------------------------------------------------*/

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 企业号JSON返回结果
    /// </summary>
    public class QyJsonResult : IJsonResult
    {
        public ReturnCode_QY errcode { get; set; }
        public string errmsg { get; set; }
        /// <summary>
        /// 为P2P返回结果做准备
        /// </summary>
        public virtual object P2PData { get; set; }
    }
}
