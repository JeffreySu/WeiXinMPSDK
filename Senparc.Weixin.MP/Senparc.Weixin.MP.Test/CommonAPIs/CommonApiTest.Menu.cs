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
                                  key = "OneClick_A",
                                  name = "单击测试",
                                  type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
                              });

            bg.button.Add(new Button()
                              {
                                  key = "OneClick_B",
                                  name = "P2P测试",
                              });

            //二级菜单
            var subButton = new Button()
                                {
                                    key = "SubClickRoot",
                                    name = "二级菜单"
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
            LoadToken();

            var result = CommonApi.GetMenu(tokenResult.access_token);

            //Assert.IsNull(result);//如果菜单不存在返回Null
            Assert.IsNotNull(result);
            Assert.IsTrue(result.menu.button.Count > 0);
        }

        [TestMethod]
        public void DeleteMenuTest()
        {
            return;//删除之后，GetMenu将返回null

            LoadToken();

            var result = CommonApi.DeleteMenu(tokenResult.access_token);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }
    }
}
