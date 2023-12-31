/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryIcpVerifyTaskResultJson.cs
    文件功能描述：查询人脸核身任务状态结果 接口返回消息
    
    
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
    /// 查询人脸核身任务状态结果
    /// </summary>
    public class QueryIcpVerifyTaskResultJson: WxJsonResult
    {
        /// <summary>
        /// 人脸核身任务是否已完成
        /// </summary>
        public bool is_finish { get; set; }

        /// <summary>
        /// 任务状态枚举：0. 未开始；1. 等待中；2. 失败；3. 成功。返回的 is_finish 字段为 true 时，face_status 才是最终状态。
        /// </summary>
        public int face_status { get; set; }
    }
}
