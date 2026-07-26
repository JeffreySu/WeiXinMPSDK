/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DraftSwitchResultJson.cs
    文件功能描述：DraftSwitchResultJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson
{
    /// <summary>
    /// 草稿箱开关状态结果
    /// </summary>
    public class DraftSwitchResultJson : WxJsonResult
    {
        /// <summary>
        /// 0 表示关闭，1 表示已开启；仅查询状态或开启成功时返回
        /// </summary>
        public int? is_open { get; set; }
    }
}
