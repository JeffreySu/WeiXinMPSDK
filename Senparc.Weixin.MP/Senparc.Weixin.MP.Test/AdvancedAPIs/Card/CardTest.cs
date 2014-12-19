using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class CardTest : CommonApiTest
    {
        protected  BaseInfo _BaseInfo=new BaseInfo()
            {
                logo_url = "http:\\www.supadmin.cn/uploads/allimg/120216/1_120216214725_1.jpg",
                brand_name = "海底捞",
                code_type = Card_CodeType.CODE_TYPE_TEXT,
                title = "132 元双人火锅套餐",
                sub_title = "",
                color = "Color010",
                notice = "使用时向服务员出示此券",
                service_phone = "020-88888888",
                description = @"不可与其他优惠同享\n 如需团购券发票，请在消费时向商户提出\n 店内均可
使用，仅限堂食\n 餐前不可打包，餐后未吃完，可打包\n 本团购券不限人数，建议2 人使用，超过建议人
数须另收酱料费5 元/位\n 本单谢绝自带酒水饮料",
                date_info = new CardCreate_DateInfo()
                {
                    type = 1,
                    begin_timestamp = DateTimeHelper.GetWeixinDateTime(DateTime.Now),
                    end_timestamp = DateTimeHelper.GetWeixinDateTime(DateTime.Now.AddDays(10)),
                },
                sku = new CardCreate_Sku()
                {
                    quantity = 5
                },
                use_limit = 1,
                get_limit = 3,
                use_custom_code = false,
                bind_openid = false,
                can_share = true,
                can_give_friend = true,
                url_name_type = Card_UrlNameType.URL_NAME_TYPE_RESERVATION,
                custom_url = "http://www.weiweihi.com",
                source = "大众点评"
            };

        [TestMethod]
        public void CreateCardTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var data = new Card_GrouponData()
                {
                    base_info = _BaseInfo,
                    deal_detail = "测试"
                };

            var result = CardCreate.CreateCard(accessToken, CardType.GROUPON, data);
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        public List<string> CardBatchGetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CardManage.CardBatchGet(accessToken, 0, 5);
            Console.Write(result);
            Assert.IsNotNull(result);
            return result.card_id_list;
        }

        [TestMethod]
        public void CreateQRTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var cardIdList = CardBatchGetTest();
            var cardId = cardIdList.FirstOrDefault();

            var result = CardCreate.CreateQR(accessToken, cardId);
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetColorsTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = CardCreate.GetColors(accessToken);
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void GetColorsTest()
        //{
        //    var accessToken = AccessTokenContainer.GetToken(_appId);

        //    var result = CardCreate.GetColors(accessToken);
        //    Console.Write(result);
        //    Assert.IsNotNull(result);
        //}
    }
}
