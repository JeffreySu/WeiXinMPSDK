/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：CreateIcpVerifyTaskResultJson.cs
    文件功能描述：发起小程序管理员人脸核身结果 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp
{
    /// <summary>
    /// 发起小程序管理员人脸核身结果
    /// </summary>
    public class CreateIcpVerifyTaskResultJson : WxJsonResult
    {
        /// <summary>
        /// 人脸核验任务id
        /// </summary>
        public string task_id { get; set; }
    }
}
