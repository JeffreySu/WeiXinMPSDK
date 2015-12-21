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

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            ButtonGroup bg = new ButtonGroup();

            //单击
            bg.button.Add(new SingleClickButton()
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
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Text",
                name = "返回文本"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_News",
                name = "返回图文"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Music",
                name = "返回音乐"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "http://weixin.senparc.com",
                name = "Url跳转"
            });
            subButton.sub_button.Add(new SingleLocationSelectButton()
            {
                key = "SingleLocationSelectButton",
                name = "位置",
            });
            bg.button.Add(subButton);


            var result = CommonApi.CreateMenu(accessToken, bg);

            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }


        [TestMethod]
        public void GetMenuTest()
        {
            //return;//已经通过测试
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = CommonApi.GetMenu(accessToken);

            //Assert.IsNull(result);//如果菜单不存在返回Null
            Assert.IsNotNull(result);
            Assert.IsTrue(result.menu.button.Count > 0);
        }

        [TestMethod]
        public void DeleteMenuTest()
        {
            return;//已经通过测试，删除之后，GetMenu将返回null

            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = CommonApi.DeleteMenu(accessToken);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }
    }
}
