using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.Card;

namespace Senparc.Weixin.MP.AdvancedAPIs.Tests
{
    [TestClass()]
    public class CardApiTests
    {
        [TestMethod()]
        public void CreateCardTest()
        {
            Card_BaseInfoBase card =new Card_BaseInfoBase();
            Card_BaseInfoBase _BaseInfo = new Card_BaseInfoBase()
            {
                logo_url = "https://mmbiz.qlogo.cn/mmbiz/GafMTen3Ue0oSEoCCjPbEZRfuJnFB5UsictIjXM2KH5S5owfCrWvjcIwCaib792mDp1KkjDWuMOicfxjfQeQXmf3g/0?wx_fmt=jpeg",
                brand_name = "花玄木里",
                code_type = Card_CodeType.CODE_TYPE_TEXT,
                title = card.title,//"132 元双人火锅套餐",
                sub_title = card.sub_title,// "周末狂欢必备",
                color = "Color010",
                notice = card.notice,// "使用时向服务员出示此券",
                service_phone = card.service_phone,// "020-88888888",
                description = card.description,
                // @"不可与其他优惠同享\n 如需团购券发票，请在消费时向商户提出\n 店内均可
                //使用，仅限堂食\n 餐前不可打包，餐后未吃完，可打包\n 本团购券不限人数，建议2 人使用，超过建议人
                //数须另收酱料费5 元/位\n 本单谢绝自带酒水饮料",
                date_info = new Card_BaseInfo_DateInfo()
                {
                    type = Card_DateInfo_Type.DATE_TYPE_FIX_TIME_RANGE.ToString(),
                    begin_timestamp = DateTimeHelper.GetWeixinDateTime(DateTime.Now),
                    end_timestamp = DateTimeHelper.GetWeixinDateTime(DateTime.Now),
                },
                sku = new Card_BaseInfo_Sku()
                {
                    quantity = 1
                },
                use_limit = card.use_limit,
                get_limit = card.get_limit,
                use_custom_code = false,
                bind_openid = false,
                can_share = true,
                can_give_friend = true,
                url_name_type = Card_UrlNameType.URL_NAME_TYPE_RESERVATION,
                custom_url = card.custom_url,//"http://www.weiweihi.com",
                source = card.source,//"大众点评",
                custom_url_name = card.custom_url_name,// "立即使用",
                custom_url_sub_title = card.custom_url_sub_title,// "6个汉字tips",
                promotion_url_name = card.promotion_url_name,//"更多优惠",
                promotion_url = card.promotion_url,//"http://www.qq.com",
                promotion_url_sub_title = card.promotion_url_sub_title
            };

            var cash = new Card_CashData()
            {
                base_info = _BaseInfo,
                least_cost = 100 * 100,
                reduce_cost = 20 * 100
            };
            try
            {
                var cardResult = CardApi.CreateCard("", cash);

                Assert.Fail();
            }
            catch (Exception ex)
            {
            }
        }
    }
}