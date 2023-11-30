#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2023 Senparc
    
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
    public class QueryAuthResultJson : WxJsonResult
    {
        /// <summary>
        /// 当前任务流程的状态码：
        /// 0: 初始状态 1: 任务超时, 24小时内有效 2: 用户授权拒绝 3: 用户授权同意 4: 发起人脸流程 5: 人脸认证失败 6: 人脸认证ok 7: 人脸认证后，已经提交手机号码下发验证码 8: 手机验证失败 9: 手机验证成功 11: 创建认证审核单失败 12: 创建认证审核审核单成功 14: 验证失败 15: 等待支付 | | auth_url | string | 小程序管理员授权链接 | | apply_status | number | 审核单状态，创建认证审核审核单成功后该值有效。 0：审核单不存在 1：待支付 2：审核中 3：打回重填 4：认证通过 5：认证最终失败（不能再修改） | | orderid | number | 小程序后台展示的认证订单号 | | appid | string | 小程序appid | | refill_reason | string | 当审核单被打回重填(apply_status=3)时有效 | | fail_reason | string | 审核最终失败的原因(apply_status=5)时有效 |
        /// </summary>
        public int task_status { get; set; }
    }
}