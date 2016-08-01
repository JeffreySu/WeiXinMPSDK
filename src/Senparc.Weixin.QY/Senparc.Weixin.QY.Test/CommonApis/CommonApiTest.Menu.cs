using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Entities.Menu;

namespace Senparc.Weixin.QY.Test.CommonApis
{
    public partial class CommonApiTest
    {
        private int _agentId = 7;

        [TestMethod]
        public void CreateMenuTest()
        {

            var accessToken = AccessTokenContainer.GetToken(_corpId);

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
            subButton.sub_button.Add(new SinglePicPhotoOrAlbumButton()
            {
                key = "SubClickRoot_Pic_Photo_Or_Album",
                name = "微信拍照"
            });
            bg.button.Add(subButton);


            var result = CommonApi.CreateMenu(accessToken, _agentId, bg);

            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }


        [TestMethod]
        public void GetMenuTest()
        {
            //return;//已经通过测试
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            var result = CommonApi.GetMenu(accessToken, _agentId);

            //Assert.IsNull(result);//如果菜单不存在返回Null
            Assert.IsNotNull(result);
            Assert.IsTrue(result.menu.button.Count > 0);
        }

        [TestMethod]
        public void DeleteMenuTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            var result = CommonApi.DeleteMenu(accessToken, _agentId);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }
    }
}
