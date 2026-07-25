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

    文件名：MiniDramaAuthorizationJson.cs
    文件功能描述：MiniDramaAuthorizationJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>查询短剧被授权信息请求。</summary>
    public class MiniDramaGetAuthorizedObjectsRequest : MiniDramaPageRequest
    {
        /// <summary>可选授权方小程序 AppId；为空时查询所有授权方。</summary>
        public string authorizer_appid { get; set; }
    }

    /// <summary>短剧剧目授权或解除授权请求。</summary>
    public class MiniDramaDramaAuthorizationRequest
    {
        /// <summary>剧目 ID 列表。</summary>
        public IList<long> drama_id { get; set; }

        /// <summary>被授权方小程序 AppId。官方“增加剧目授权”参数表误写为 <c>authorized</c>，示例及关联接口均使用本字段名。</summary>
        public string authorized_appid { get; set; }

        /// <summary>可选授权到期时间戳；不传或传 0 表示永久有效。</summary>
        public long? authz_expire_time { get; set; }
    }

    /// <summary>解除短剧剧目授权请求。</summary>
    public class MiniDramaDramaDeauthorizationRequest
    {
        /// <summary>待解除授权的剧目 ID 列表。</summary>
        public IList<long> drama_id { get; set; }

        /// <summary>被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }
    }

    /// <summary>查询短剧授权信息请求。</summary>
    public class MiniDramaGetAuthorizeObjectsRequest : MiniDramaPageRequest
    {
        /// <summary>可选剧目 ID；为空时查询所有剧目。</summary>
        public long? drama_id { get; set; }

        /// <summary>可选被授权方小程序 AppId；为空时查询所有被授权方。</summary>
        public string authorized_appid { get; set; }
    }

    /// <summary>短剧账号授权请求。</summary>
    public class MiniDramaAppAuthorizationRequest
    {
        /// <summary>被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>可选授权到期时间戳；不传或传 0 表示永久有效。</summary>
        public long? authz_expire_time { get; set; }
    }

    /// <summary>短剧账号标识请求。</summary>
    public class MiniDramaAppIdentityRequest
    {
        /// <summary>被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }
    }

    /// <summary>短剧授权操作结果项。</summary>
    public class MiniDramaAuthorizationOperationResult
    {
        /// <summary>剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>该剧目授权操作错误码，0 表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>该剧目授权操作错误原因。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>短剧授权操作结果。</summary>
    public class MiniDramaAuthorizationJsonResult : WxJsonResult
    {
        /// <summary>逐剧目授权操作结果。</summary>
        public IList<MiniDramaAuthorizationOperationResult> result { get; set; }
    }

    /// <summary>短剧被授权信息。</summary>
    public class MiniDramaAuthorizedObject
    {
        /// <summary>授权剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>授权方小程序 AppId。</summary>
        public string authorizer_appid { get; set; }

        /// <summary>授权时间戳。</summary>
        public long authorized_time { get; set; }

        /// <summary>授权到期时间戳，0 表示长期有效。</summary>
        public long authz_expire_time { get; set; }
    }

    /// <summary>短剧被授权信息结果。</summary>
    public class MiniDramaGetAuthorizedObjectsJsonResult : WxJsonResult
    {
        /// <summary>记录总数。</summary>
        public int total_count { get; set; }

        /// <summary>被授权信息列表。</summary>
        public IList<MiniDramaAuthorizedObject> objects { get; set; }
    }

    /// <summary>短剧授权信息。</summary>
    public class MiniDramaAuthorizeObject
    {
        /// <summary>授权剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>授权时间戳。</summary>
        public long authorized_time { get; set; }

        /// <summary>授权到期时间戳，0 表示长期有效。</summary>
        public long authz_expire_time { get; set; }
    }

    /// <summary>短剧授权信息查询结果。</summary>
    public class MiniDramaGetAuthorizeObjectsJsonResult : WxJsonResult
    {
        /// <summary>记录总数。</summary>
        public int total_count { get; set; }

        /// <summary>授权信息列表。</summary>
        public IList<MiniDramaAuthorizeObject> objects { get; set; }
    }

    /// <summary>短剧账号授权信息。</summary>
    public class MiniDramaAuthorizedApp
    {
        /// <summary>被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>授权时间戳。</summary>
        public long authorized_time { get; set; }

        /// <summary>授权到期时间戳，0 表示长期有效。</summary>
        public long authz_expire_time { get; set; }
    }

    /// <summary>短剧账号授权信息查询结果。</summary>
    public class MiniDramaGetAuthorizeAppsJsonResult : WxJsonResult
    {
        /// <summary>账号授权信息列表。</summary>
        public IList<MiniDramaAuthorizedApp> objects { get; set; }
    }

    /// <summary>短剧版权授权或解除授权请求。</summary>
    public class MiniDramaCopyrightAuthorizationRequest
    {
        /// <summary>授权类型：1 授权给主体，2 授权给小程序。</summary>
        public int authorization_type { get; set; }

        /// <summary>授权类型为 2 时必填的被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>授权类型为 1 时必填的被授权方统一社会信用代码。</summary>
        public string authorized_subject_cert_no { get; set; }

        /// <summary>受版权保护的剧目 ID 列表，最多 100 个。</summary>
        public IList<long> drama_ids { get; set; }

        /// <summary>可选授权到期时间戳，0 表示永久有效；指定时授权时长不能少于 7 天。</summary>
        public long? expire_time { get; set; }
    }

    /// <summary>解除短剧版权授权请求。</summary>
    public class MiniDramaCopyrightDeauthorizationRequest
    {
        /// <summary>被授权方类型：1 主体，2 小程序。</summary>
        public int authorization_type { get; set; }

        /// <summary>授权类型为 2 时必填的被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>授权类型为 1 时必填的被授权方统一社会信用代码。</summary>
        public string authorized_subject_cert_no { get; set; }

        /// <summary>待解除授权的受版权保护剧目 ID 列表，最多 100 个。</summary>
        public IList<long> drama_ids { get; set; }
    }

    /// <summary>短剧版权授权信息查询请求。</summary>
    public class MiniDramaGetCopyrightAuthorizationListRequest : MiniDramaPageRequest
    {
        /// <summary>可选授权类型：0 全部、1 主体、2 小程序。</summary>
        public int? authorization_type { get; set; }

        /// <summary>授权类型为 2 时使用的被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>授权类型为 1 时使用的被授权方统一社会信用代码。</summary>
        public string authorized_subject_cert_no { get; set; }

        /// <summary>可选授权剧目 ID，空或 0 表示全部。</summary>
        public long? drama_id { get; set; }
    }

    /// <summary>查询自己被版权授权的信息请求。</summary>
    public class MiniDramaGetCopyrightAuthorizedListRequest : MiniDramaPageRequest
    {
        /// <summary>可选授权方小程序 AppId；为空时查询全部。</summary>
        public string authorizer_appid { get; set; }
    }

    /// <summary>短剧版权授权信息。</summary>
    public class MiniDramaCopyrightAuthorizationInfo
    {
        /// <summary>授权方小程序 AppId。</summary>
        public string authorizer_appid { get; set; }

        /// <summary>授权类型：1 主体，2 小程序。</summary>
        public int authorization_type { get; set; }

        /// <summary>被授权方小程序 AppId。</summary>
        public string authorized_appid { get; set; }

        /// <summary>被授权方主体统一社会信用代码。</summary>
        public string authorized_subject_cert_no { get; set; }

        /// <summary>授权剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>授权时间戳。</summary>
        public long authorized_time { get; set; }

        /// <summary>授权到期时间戳，0 表示长期有效。</summary>
        public long expire_time { get; set; }
    }

    /// <summary>短剧版权授权信息查询结果。</summary>
    public class MiniDramaCopyrightAuthorizationListJsonResult : WxJsonResult
    {
        /// <summary>记录总数。</summary>
        public int total_count { get; set; }

        /// <summary>版权授权信息列表。</summary>
        public IList<MiniDramaCopyrightAuthorizationInfo> list { get; set; }
    }
}
