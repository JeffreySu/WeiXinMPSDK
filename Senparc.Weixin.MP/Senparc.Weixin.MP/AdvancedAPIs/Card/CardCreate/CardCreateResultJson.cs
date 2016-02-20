/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CardCreateResultJson.cs
    文件功能描述：创建卡券返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150323
    修改描述：添加上传logo返回结果
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建卡券返回结果
    /// </summary>
    public class CardCreateResultJson : WxJsonResult
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
       public string card_id { get; set; } 
    }

    /// <summary>
    /// 获取颜色列表返回结果
    /// </summary>
    public class GetColorsResultJson : WxJsonResult
    {
        /// <summary>
        /// 颜色列表
        /// </summary>
        public List<Card_Color> colors { get; set; }
    }
    
    public class Card_Color
    {
        /// <summary>
        /// 可以填入的color 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 对应的颜色数值
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 生成卡券二维码返回结果
    /// </summary>
    public class CreateQRResultJson : WxJsonResult
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket 可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
    }

    /// <summary>
    /// 消耗code返回结果
    /// </summary>
    public class CardConsumeResultJson : WxJsonResult
    {
        public CardId card { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }

    public class CardId
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// code 解码
    /// </summary>
    public class CardDecryptResultJson : WxJsonResult
    {
        public string code { get; set; }
    }

    /// <summary>
    /// 上传logo返回结果
    /// </summary>
    public class Card_UploadLogoResultJson : WxJsonResult
    {
        public string url { get; set; }
    }
}
