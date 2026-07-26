/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CurrentContactJson.cs
    文件功能描述：企业微信当前通讯录补充模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐加入企业二维码与单个部门详情模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList
{
    /// <summary>加入企业二维码结果。</summary>
    public class GetJoinQrcodeResult : WorkJsonResult
    {
        public string join_qrcode { get; set; }
    }

    /// <summary>单个部门详情结果。</summary>
    public class GetDepartmentResult : WorkJsonResult
    {
        public DepartmentDetail department { get; set; }
    }

    /// <summary>企业微信部门详情。</summary>
    public class DepartmentDetail
    {
        public long id { get; set; }
        public string name { get; set; }
        public string name_en { get; set; }
        public IList<string> department_leader { get; set; }
        public long parentid { get; set; }
        public long order { get; set; }
    }
}
