using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Work.AdvancedAPIs.Mass.SendTemplateCard;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MassApi
{
    [TestClass]
    public class MassApiTests : CommonApiTest
    {
        [TestMethod]
        public void SendNewsTest()
        {
            /* 说明：此方法仅用于测试AccessToken是否可以被正确传递：
             * https://github.com/JeffreySu/WeiXinMPSDK/issues/964#issuecomment-351890440 */

            try
            {
                var accessToken =
                    "fM5CruClicaXYpz9vai-nl2lB2V-S25Yed_BDmd8sl6P1vBvExBnfoYbOrYAMEawhZc5bhX1mu0nVJilrBxXyeAs-7y70gkwJIEXVrk4JvfFaDJP6GdWBDYq5l6tqfL8megghLqDHLXPsdokIJ6UmXrb8buPEUgdKPNWXNJKVeq32uqB54OaAnxNkQVK-MtbI_QgNT4grhUhuIbYak_7Gg";

                var result = Work.AdvancedAPIs.MassApi.SendNews(accessToken, "agentId", new List<Article>());
                Assert.Fail();
            }
            catch (Senparc.Weixin.Exceptions.UnRegisterAppIdException e)
            {
                Assert.Fail(); //出现反馈问题中的“AppId未注册”错误
            }
            catch (Senparc.Weixin.Exceptions.ErrorJsonResultException e)
            {
                //请求发生错误！错误代码：40014，说明：invalid access_token”
                Assert.AreEqual(40014, (int)e.JsonResult.errcode);

                //没有发生AppId未注册的错误
            }
            catch (Exception e)
            {
                Assert.Fail();//其他错误
            }
        }


        [TestMethod()]
        public void SendTemplateCardTest()
        {
            var corpId = base._corpId;

            var card = new Template_Card_Text()
            {
                source = new Source()
                {
                    icon_url = "https://sdk.weixin.senparc.com/images/v2/ewm_01.png",
                    desc = "Senparc.Weixin SDK 单元测试",
                    desc_color = 1
                },
                action_menu = new()
                {
                    desc = "盛派 SDK 说明",
                    action_list = new Action_List[] {
                        new() { text = "查看", key = "A" },
                        new() { text = "不查看", key = "B" },
                    }
                },
                task_id = SystemTime.Now.Ticks.ToString(),
                main_title = new() { title = "欢迎使用 Senparc.Weixin SDK", desc = "开源地址：https://github.com/JeffreySu/WeiXinMPSDK" },
                quote_area = new() { type = 1, url = "https://sdk.weixin.senparc.com", title = "在线示例", quote_text = "100% 开源" },
                emphasis_content = new() { title = "开源协议", desc = "Apache License Version 2.0" },
                sub_title_text = "您可以加 QQ 或微信群及时了解更多最新情况！",
                horizontal_content_list = new Horizontal_Content_List[] {
                    new() { keyname = "邀请人", value = "Jeffrey" },
                    new() { type = 1, keyname = "在线示例", value = "点击访问", url = "https://sdk.weixin.senparc.com" },
                    //new(){  type=2, keyname="在线示例", value="点击访问", media_id="xxxx"},
                    new() { type = 3, keyname = "员工信息", value = "点击查看", userid = "001" },
                },
                jump_list = new Jump_List[] {
                    new() { type = 1, title = "GitHub 开源地址", url = "https://github.com/JeffreySu/WeiXinMPSDK" },
                    new() { type = 2, title = "跳转到小程序", appid = "wx12b4f63276b14d4c", pagepath = "/index/index" }
                },
                card_action = new() { type = 2, url = "https://sdk.weixin.senparc.com", appid = "wx12b4f63276b14d4c", pagepath = "/index/index" },
            };

            SendTemplateCardRequest requestData = new(card)
            {
                touser = "001",
                toparty = "29",
                totag = null,
                enable_id_trans = 0,
                enable_duplicate_check = 0,
                duplicate_check_interval = 1800
            };

            var appKey = Senparc.Weixin.Work.Containers.AccessTokenContainer.BuildingKey(base._corpId, base._corpSecret);

            var result = Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTemplateCard(appKey, requestData);
            Assert.IsNotNull(result);
            Console.WriteLine(result);
        }
    }
}
