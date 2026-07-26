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

    文件名：NovelAuthorizationJson.cs
    文件功能描述：NovelAuthorizationJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Novel
{
    /// <summary>小说作品授权项。</summary>
    public class NovelBookAuthorizationInput
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>被授权账号 AppId。</summary>
        public string grantee_appid { get; set; }

        /// <summary>授权到期 Unix 时间戳，单位秒，最大 2147483646。</summary>
        public long expire_time { get; set; }
    }

    /// <summary>新增账号与小说授权请求。</summary>
    public class NovelAddBookAuthorizationRequest
    {
        /// <summary>授权关系列表，单次最多 20 条。</summary>
        public IList<NovelBookAuthorizationInput> books { get; set; }
    }

    /// <summary>单条授权操作结果。</summary>
    public class NovelAuthorizationOperationResult
    {
        /// <summary>单条授权关系的错误码，0 表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>单条授权关系的错误信息；部分接口会返回。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>批量授权结果。</summary>
    public class NovelAuthorizationJsonResult : WxJsonResult
    {
        /// <summary>每条授权关系的处理结果。</summary>
        public IList<NovelAuthorizationOperationResult> results { get; set; }
    }

    /// <summary>查询账号与小说授权关系列表请求。</summary>
    public class NovelQueryBookAuthorizationRequest
    {
        /// <summary>0 或不设置查询授权列表，1 查询被授权列表。</summary>
        public int? type { get; set; }

        /// <summary>结果偏移位置，从 0 开始。</summary>
        public int offset { get; set; }

        /// <summary>记录数量，单次最多 30 条。</summary>
        public int count { get; set; }

        /// <summary>是否返回总数；查询被授权列表时无需设置。</summary>
        public bool? is_sum { get; set; }

        /// <summary>可选作品 ID 筛选；查询被授权列表时无需设置。</summary>
        public string book_id { get; set; }
    }

    /// <summary>小说授权关系。</summary>
    public class NovelBookAuthorizationInfo
    {
        /// <summary>作品 ID；账号级授权列表中可能不返回。</summary>
        public string book_id { get; set; }

        /// <summary>主授权账号 AppId。</summary>
        public string grantor_appid { get; set; }

        /// <summary>被授权账号 AppId。</summary>
        public string grantee_appid { get; set; }

        /// <summary>授权到期 Unix 时间戳，单位秒。</summary>
        public long expire_time { get; set; }

        /// <summary>授权记录总数；仅旧版查询设置 is_sum=true 时可能返回。</summary>
        public int? sum { get; set; }
    }

    /// <summary>账号与小说授权关系列表结果。</summary>
    public class NovelQueryBookAuthorizationJsonResult : WxJsonResult
    {
        /// <summary>授权关系列表。</summary>
        public IList<NovelBookAuthorizationInfo> results { get; set; }
    }

    /// <summary>删除指定小说授权关系请求。</summary>
    public class NovelDeleteBookAuthorizationRequest
    {
        /// <summary>作品 ID。</summary>
        public string book_id { get; set; }

        /// <summary>被授权账号 AppId。</summary>
        public string grantee_appid { get; set; }
    }

    /// <summary>账号级小说授权项。</summary>
    public class NovelAppAuthorizationInput
    {
        /// <summary>被授权账号 AppId。</summary>
        public string grantee_appid { get; set; }

        /// <summary>授权到期 Unix 时间戳，单位秒。</summary>
        public long expire_time { get; set; }
    }

    /// <summary>新增账号级小说授权请求。</summary>
    public class NovelAddAppAuthorizationRequest
    {
        /// <summary>被授权账号列表，单次最多 20 条。</summary>
        public IList<NovelAppAuthorizationInput> infos { get; set; }
    }

    /// <summary>查询账号级小说授权关系请求。</summary>
    public class NovelQueryAppAuthorizationRequest
    {
        /// <summary>0 或不设置查询授权列表，1 查询被授权列表。</summary>
        public int? type { get; set; }

        /// <summary>
        /// 可选记录数量，单次最多 100 条。官方参数表标为必填，但“按指定小说查询”的示例未提交该字段。
        /// </summary>
        public int? count { get; set; }

        /// <summary>分页游标；空值表示第一页。</summary>
        public string cursor { get; set; }

        /// <summary>可选主授权账号 AppId，用于查询指定授权方授权的小说。</summary>
        public string grantor_appid { get; set; }

        /// <summary>可选作品 ID 列表，最多 30 个；优先级高于 grantor_appid。</summary>
        public IList<string> book_ids { get; set; }
    }

    /// <summary>账号级小说授权关系列表结果。</summary>
    public class NovelQueryAppAuthorizationJsonResult : WxJsonResult
    {
        /// <summary>账号授权关系列表。</summary>
        public IList<NovelBookAuthorizationInfo> appid_results { get; set; }

        /// <summary>按授权方或作品查询时返回的小说授权关系列表；官方返回表未列出，但示例返回。</summary>
        public IList<NovelBookAuthorizationInfo> book_results { get; set; }

        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>删除账号级小说授权关系请求。</summary>
    public class NovelDeleteAppAuthorizationRequest
    {
        /// <summary>被授权账号 AppId。</summary>
        public string grantee_appid { get; set; }
    }
}
