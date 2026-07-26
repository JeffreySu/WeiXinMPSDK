#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：StudentJson.cs
    文件功能描述：StudentJson 强类型数据模型


    创建标识：Senparc - 20131202

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Student
{
    /// <summary>快速获取学生身份请求。</summary>
    public class QuickCheckStudentIdentityRequest
    {
        /// <summary>用户在当前小程序下的 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>学生身份快速验证插件返回的授权 Code，有效期两小时。</summary>
        public string wx_studentcheck_code { get; set; }
    }

    /// <summary>快速获取学生身份结果。</summary>
    public class QuickCheckStudentIdentityJsonResult : WxJsonResult
    {
        /// <summary>绑定状态：1 未绑定，2 审核中，3 已绑定。</summary>
        public int bind_status { get; set; }

        /// <summary>是否为学生。</summary>
        public bool is_student { get; set; }
    }
}
