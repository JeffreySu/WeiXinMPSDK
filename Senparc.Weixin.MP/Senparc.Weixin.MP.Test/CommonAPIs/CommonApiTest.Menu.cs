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
            return;//已经通过测试

            LoadToken();

            ButtonGroup bg = new ButtonGroup();

            //单击
            bg.button.Add(new SingleButton()
                              {
                                  name = "单击测试",
                                  key = "OneClick",
                                  type = ButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
                              });

            //二级菜单
            var subButton = new SubButton()
                                {
                                    name = "二级菜单"
                                };
            subButton.sub_button.Add(new SingleButton()
                                        {
                                            key = "SubClickRoot_Text",
                                            name = "返回文本"
                                        });
            subButton.sub_button.Add(new SingleButton()
                                        {
                                            key = "SubClickRoot_News",
                                            name = "返回图文"
                                        });
            subButton.sub_button.Add(new SingleButton()
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
            return;//已经通过测试
            LoadToken();

            //绕过LoadToken()，减少请求次数
            //this.tokenResult = new AccessTokenResult()
            //{
            //    //根据获得的实际有效的Token进行填写
            //    access_token = "T1vpXEjgjhESumTE4tilUdszVSWj0ZP7AsrLPJi99cTVGLMI07Rsrc3pl8tum7rJzNdrysA4-DpCGUYKrPnsuKVNVFtejU7ahKCEtZ0HtgRhVSkZ1LLflIglIWvzg0Lt8TW1FLEaepRpmACkmm6YJw"
            //};

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
