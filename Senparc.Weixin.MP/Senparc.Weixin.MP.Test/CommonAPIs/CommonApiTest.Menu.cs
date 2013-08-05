using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    public partial class CommonApiTest
    {
        [TestMethod]
        public void CreateMenuTest()
        {
            LoadToken();

            ButtonGroup bg = new ButtonGroup()
                                 {
                                     button = new List<Button>()
                                 };

            //单击
            bg.button.Add(new Button()
                              {
                                  key = "OneClick",
                                  name = "单击测试",
                                  type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
                              });
            //二级菜单
            var subButton = new Button()
                                {
                                    key = "SubClickRoot",
                                    name = "二级菜单测试"
                                };
            subButton.sub_button.Add(new SubButton()
                                        {
                                            key = "SubClickRoot_Test",
                                            name = "返回文本"
                                        });
            subButton.sub_button.Add(new SubButton()
                                        {
                                            key = "SubClickRoot_News",
                                            name = "返回图文"
                                        });
            subButton.sub_button.Add(new SubButton()
                                        {
                                            key = "SubClickRoot_Music",
                                            name = "返回音乐"
                                        });
            bg.button.Add(subButton);

            var result = CommonApi.CreateMenu(tokenResult.access_token, bg);

            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }


        [TestMethod]
        public void GetMenuTest()
        {

        }
    }
}
