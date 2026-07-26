/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ChangeOpenIdJsonResult.cs
    文件功能描述：ChangeOpenIdJsonResult 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.User
{
    /// <summary>
    /// 账号迁移 OpenId 转换结果
    /// </summary>
    public class ChangeOpenIdJsonResult : WxJsonResult
    {
        public ChangeOpenIdResultItem[] result_list { get; set; }
    }

    /// <summary>
    /// ChangeOpenIdResult 数据项。
    /// </summary>
    public class ChangeOpenIdResultItem
    {
        public string ori_openid { get; set; }
        public string new_openid { get; set; }
        public string err_msg { get; set; }
    }
}
