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

    文件名：SoterJson.cs
    文件功能描述：SoterJson 强类型数据模型


    创建标识：Senparc - 20131202

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Soter
{
    /// <summary>SOTER 生物认证签名验证请求。</summary>
    public class SoterVerifySignatureRequest
    {
        /// <summary>发起生物认证用户的小程序 OpenId。</summary>
        public string openid { get; set; }

        /// <summary><c>wx.startSoterAuthentication</c> 成功回调的 resultJSON 字段。</summary>
        public string json_string { get; set; }

        /// <summary><c>wx.startSoterAuthentication</c> 成功回调的 resultJSONSignature 字段。</summary>
        public string json_signature { get; set; }
    }

    /// <summary>SOTER 生物认证签名验证结果。</summary>
    public class SoterVerifySignatureJsonResult : WxJsonResult
    {
        /// <summary>生物认证签名是否验证通过。</summary>
        public bool is_ok { get; set; }
    }
}
