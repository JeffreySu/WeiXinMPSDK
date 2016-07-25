/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CardUpdateData.cs
    文件功能描述：卡券更新需要的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建货架数据
    /// </summary>
    public class ShelfCreateData
    {
        /// <summary>
        /// 页面的banner图片链接，须调用。
        /// </summary>
        public string banner { get; set; }
        /// <summary>
        /// 页面的title。
        /// </summary>
        public string page_title { get; set; }
        /// <summary>
        /// 页面是否可以分享,填入true/false
        /// </summary>
        public bool can_share { get; set; }
        /// <summary>
        /// 投放页面的场景值；SCENE_NEAR_BY 附近 SCENE_MENU	自定义菜单 SCENE_QRCODE	二维码 SCENE_ARTICLE	公众号文章 SCENE_H5	h5页面 SCENE_IVR	自动回复 SCENE_CARD_CUSTOM_CELL	卡券自定义cell
        /// </summary>
        public CardShelfCreate_Scene scene { get; set; }
        /// <summary>
        /// 卡券列表
        /// </summary>
        public List<ShelfCreateData_CardList> card_list { get; set; }
    }

    public class ShelfCreateData_CardList
    {
        /// <summary>
        /// 所要在页面投放的cardid
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 缩略图url
        /// </summary>
        public string thumb_url { get; set; }
    }
}