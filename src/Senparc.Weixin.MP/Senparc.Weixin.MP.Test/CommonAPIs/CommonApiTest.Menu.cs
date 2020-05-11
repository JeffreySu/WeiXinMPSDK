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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
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

            //var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            ButtonGroup bg = new ButtonGroup();

            //二级菜单
            var subButton = new SubButton()
            {
                name = "二级菜单"
            };
            bg.button.Add(subButton);

            subButton.sub_button.Add(new SingleViewButton()
            {
                 url = "https://book.weixin.senparc.com/book/link?code=SenparcRobotMenu",
                name = "《微信开发深度解析》"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "https://sdk.weixin.senparc.com/TenpayV3/ProductList",
                name = "微信支付"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "Description",
                name = "测试使用说明"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "OneClick",
                name = "单击测试"
            });
            subButton.sub_button.Add(new SingleViewButton()
            {
                url = "https://weixin.senparc.com/",
                name = "Url跳转"
            });

            //二级菜单
            var subButton2 = new SubButton()
            {
                name = "二级菜单"
            };
            bg.button.Add(subButton2);

            subButton2.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Text",
                name = "返回文本"
            });
            subButton2.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_News",
                name = "返回图文"
            });
            subButton2.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Music",
                name = "返回音乐"
            });
            subButton2.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Image",
                name = "返回图片"
            });
            subButton2.sub_button.Add(new SingleClickButton()
            {
                key = "OAuth",
                name = "OAuth2.0授权测试"
            });


            //二级菜单
            var subButton3 = new SubButton()
            {
                name = "更多"
            };
            bg.button.Add(subButton3);

            subButton3.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Agent",
                name = "代理消息-返回图文"
            });

            var result = CommonApi.CreateMenu(_appId, bg);

            Assert.IsNotNull(result);
            Assert.AreEqual("ok", result.errmsg);
        }


        [TestMethod]
        public void GetMenuTest()
        {
            return;//已经通过测试
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
