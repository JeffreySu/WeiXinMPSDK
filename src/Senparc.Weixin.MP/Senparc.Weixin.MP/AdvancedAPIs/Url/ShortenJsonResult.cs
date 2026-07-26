/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ShortenJsonResult.cs
    文件功能描述：ShortenJsonResult 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Url
{
    /// <summary>
    /// 长信息转短链结果
    /// </summary>
    public class GenerateShortenJsonResult : WxJsonResult
    {
        public string short_key { get; set; }
    }

    /// <summary>
    /// 短链还原长信息结果
    /// </summary>
    public class FetchShortenJsonResult : WxJsonResult
    {
        public string long_data { get; set; }
        public long create_time { get; set; }
        public int expire_seconds { get; set; }
    }
}
