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

    文件名：FaceJson.cs
    文件功能描述：FaceJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Face
{
    /// <summary>人脸核身用户证件信息。</summary>
    public class FaceCertificateInfo
    {
        /// <summary>证件类型；居民身份证固定填写 <c>IDENTITY_CARD</c>。</summary>
        public string cert_type { get; set; }

        /// <summary>证件姓名，必须使用 UTF-8 编码。</summary>
        public string cert_name { get; set; }

        /// <summary>证件号码。</summary>
        public string cert_no { get; set; }
    }

    /// <summary>获取人脸核身会话标识请求。</summary>
    public class FaceGetVerifyIdRequest
    {
        /// <summary>业务系统流水号；长度 5 至 32，只能包含数字、大小写字母、下划线和连字符，且在同一 AppId 下唯一。</summary>
        public string out_seq_no { get; set; }

        /// <summary>用户证件信息。</summary>
        public FaceCertificateInfo cert_info { get; set; }

        /// <summary>当前小程序用户 OpenId。</summary>
        public string openid { get; set; }
    }

    /// <summary>获取人脸核身会话标识结果。</summary>
    public class FaceGetVerifyIdJsonResult : WxJsonResult
    {
        /// <summary>微信生成的人脸核身会话唯一标识，最长 256 个字符。</summary>
        public string verify_id { get; set; }

        /// <summary>会话标识有效期，单位为秒，默认 3600 秒。</summary>
        public int expires_in { get; set; }
    }

    /// <summary>查询人脸核身真实验证结果请求。</summary>
    public class FaceQueryVerifyInfoRequest
    {
        /// <summary>获取会话标识接口返回的 VerifyId。</summary>
        public string verify_id { get; set; }

        /// <summary>业务系统流水号，必须与获取会话标识时传入的值一致。</summary>
        public string out_seq_no { get; set; }

        /// <summary>证件信息摘要；可使用 <see cref="FaceApi.CreateCertificateHash"/> 生成。</summary>
        public string cert_hash { get; set; }

        /// <summary>用户 OpenId，必须与获取会话标识时传入的值一致。</summary>
        public string openid { get; set; }
    }

    /// <summary>查询人脸核身真实验证结果。</summary>
    public class FaceQueryVerifyInfoJsonResult : WxJsonResult
    {
        /// <summary>人脸核身验证结果；10000 表示成功，10005 表示处理中，10300 表示尚未完成，其他值见官方枚举。</summary>
        /// <remarks>只有 <c>errcode == 0</c> 且 <c>verify_ret == 10000</c> 才表示核身通过。</remarks>
        public int verify_ret { get; set; }
    }
}
