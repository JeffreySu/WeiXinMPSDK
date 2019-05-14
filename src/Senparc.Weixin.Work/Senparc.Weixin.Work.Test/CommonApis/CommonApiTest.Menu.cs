#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Entities.Menu;

namespace Senparc.Weixin.Work.Test.CommonApis
{
    public partial class CommonApiTest
    {
        private int _agentId = 7;

        [TestMethod]
        public void CreateMenuTest()
        {

            var accessToken = AccessTokenContainer.GetToken(_corpId, _corpSecret);

            ButtonGroup bg = new ButtonGroup();

            //单击
            bg.button.Add(new SingleClickButton()
            {
                name = "单击测试",
                key = "OneClick",
                type = MenuButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
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
                url = "https://weixin.senparc.com",
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
            var accessToken = AccessTokenContainer.GetToken(_corpId, _corpSecret);

            var result = CommonApi.GetMenu(accessToken, _agentId);

            //Assert.IsNull(result);//如果菜单不存在返回Null
            Assert.IsNotNull(result);
            Assert.IsTrue(result.menu.button.Count > 0);
        }

        [TestMethod]
        public void DeleteMenuTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, _corpSecret);

            var result = CommonApi.DeleteMenu(accessToken, _agentId);
            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }
    }
}
