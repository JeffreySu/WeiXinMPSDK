/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：GetAuditStatusResultJson.cs
    文件功能描述：审核ID返回结果
    
    
    创建标识：Senparc - 20170726

    注意：此项目是《微信开发深度解析：微信公众号、小程序高效开发秘籍》图书中第5章的WeixinMarketing项目源代码。
    本项目只包含了运行案例所必须的学习代码，以及精简的部分SenparcCore框架代码，不确保其他方面的稳定性、安全性，
    因此，请勿直接用于商业项目，例如安全性、缓存等需要根据具体情况进行调试。

    盛派网络保留所有权利。

----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetAuditStatusResultJson : WxJsonResult
    {

        /// <summary>
        /// 最新的审核ID，只在使用GetLatestAuditStatus接口时才有返回值
        /// </summary>
        public string auditid { get; set; }
        /// <summary>
        /// 审核状态，其中0为审核成功，1为审核失败，2为审核中
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 当status=1，审核被拒绝时，返回的拒绝原因
        /// </summary>
        public string reason { get; set; }
    }
}
