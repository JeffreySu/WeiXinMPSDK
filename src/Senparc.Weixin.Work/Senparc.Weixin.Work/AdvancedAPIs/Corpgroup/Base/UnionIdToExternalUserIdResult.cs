using Senparc.Weixin.Entities;

/* 项目“Senparc.Weixin.Work.net6 (netstandard2.1)”的未合并的更改
在此之前:
using System.Collections.Generic;
在此之后:
using Senparc.Weixin.Work;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp;
using System.Collections.Generic;
*/
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 通过unionid和openid查询external_userid 响应参数
    /// </summary>
    public class UnionIdToExternalUserIdResult : WorkJsonResult
    {
        /// <summary>
        /// 该unionid对应的外部联系人信息
        /// </summary>
        public List<UnionIdToExternalUserIdResult_ExternalUserIdInfo> external_userid_info { get; set; }
    }

    public class UnionIdToExternalUserIdResult_ExternalUserIdInfo
    {
        /// <summary>
        /// 所属企业id
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 外部联系人id
        /// </summary>
        public string external_userid { get; set; }
    }
}
