/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CardCreateInfo.cs
    文件功能描述：创建卡券数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建卡券数据
    /// </summary>
    public class CardCreateInfo
    {
        #region JSON示例

        /*
{ "card": {
            "card_type": "GROUPON",
            "groupon": {
                        "base_info": {
                                        "logo_url":"http:\/\/www.supadmin.cn\/uploads\/allimg\/120216\/1_120216214725_1.jpg",
                                        "brand_name":"海底捞",
                                        "code_type":"CODE_TYPE_TEXT",
                                        "title": "132 元双人火锅套餐",
                                        "sub_title": "",
                                        "color": "Color010",
                                        "notice": "使用时向服务员出示此券",
                                        "service_phone": "020-88888888",
                                        "description": "不可与其他优惠同享\n 如需团购券发票，请在消费时向商户提出\n 店内均可
                                        使用，仅限堂食\n 餐前不可打包，餐后未吃完，可打包\n 本团购券不限人数，建议2 人使用，超过建议人
                                        数须另收酱料费5 元/位\n 本单谢绝自带酒水饮料",
                                        "date_info": {
                                                    "type": 1,
                                                    "begin_timestamp": 1397577600 ,
                                                    "end_timestamp": 1422724261
                                        },
                                        "sku": {
                                                    "quantity": 50000000
                                        },
                                        "get_limit": 3,
                                        "use_custom_code": false,
                                        "bind_openid": false,
                                        "can_share": true,
                                        "can_give_friend": true,
                                        "location_id_list" : [123, 12321, 345345],
                                        "url_name_type": "URL_NAME_TYPE_RESERVATION",
                                        "custom_url": "http://www.qq.com",
                                        "source": "大众点评"
                        },
                        "deal_detail": "以下锅底2 选1（有菌王锅、麻辣锅、大骨锅、番茄锅、清补凉锅、酸菜鱼锅可
                        选）：\n 大锅1 份12 元\n 小锅2 份16 元\n 以下菜品2 选1\n 特级肥牛1 份30 元\n 洞庭鮰鱼卷1 份
                        20 元\n 其他\n 鲜菇猪肉滑1 份18 元\n 金针菇1 份16 元\n 黑木耳1 份9 元\n 娃娃菜1 份8 元\n 冬
                        瓜1 份6 元\n 火锅面2 个6 元\n 欢乐畅饮2 位12 元\n 自助酱料2 位10 元"
                        }
            }
}
     */

        #endregion

        /// <summary>
        /// 卡券信息部分
        /// </summary>
        public CardCreateInfo_Card card { get; set; }
    }
}
