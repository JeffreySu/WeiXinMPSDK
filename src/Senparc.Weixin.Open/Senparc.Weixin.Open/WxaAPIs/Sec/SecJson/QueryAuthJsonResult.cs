#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryAuthResultJson.cs
    文件功能描述：小程序认证进度查询
    
    
    创建标识：Yaofeng - 20231130

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxaAPIs.Sec
{
    /// <summary>
    /// 小程序认证进度查询
    /// </summary>
    public class QueryAuthJsonResult : WxJsonResult
    {
        /// <summary>
        /// 当前任务流程的状态码：
        /// </summary>
        public TaskStatus task_status { get; set; }

        /// <summary>
        /// 小程序管理员授权链接
        /// </summary>

        public string auth_url { get; set; }

        /// <summary>
        /// 审核单状态，创建认证审核审核单成功后该值有效
        /// </summary>
        public ApplyStatus apply_status { get; set; }

        /// <summary>
        /// 小程序后台展示的认证订单号
        /// </summary>
        public string orderid { get; set; }

        /// <summary>
        /// 小程序appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 当审核单被打回重填(apply_status=3)时有效
        /// </summary>
        public string refill_reason { get; set; }

        /// <summary>
        /// 审核最终失败的原因(apply_status=5)时有效
        /// </summary>
        public string fail_reason { get; set; }

        public enum TaskStatus
        {
            初始状态 =0,
            任务超时_24小时内有效=1,
            用户授权拒绝 = 2,
            用户授权同意=3,
            发起人脸流程=4,
            人脸认证失败=5,
            人脸认证ok = 6,
            人脸认证后_已经提交手机号码下发验证码= 7,
            手机验证失败=8,
            手机验证成功 =9,
            创建认证审核单失败=11,
            创建认证审核审核单成功 =12,
            验证失败 =14,
            等待支付=15
        }

        public enum ApplyStatus
        {
            审核单不存在 = 0,
            待支付 = 1, 
            审核中 =  2,
            打回重填 = 3,
            认证通过 = 4,
            认证最终失败_不能再修改 = 5
        }
    }
}