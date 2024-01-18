#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：SingleArticleIdButton.cs
    文件功能描述：用户点击 article_id 类型按钮后，微信客户端将会以卡片形式，下发开发者在按钮中填写的图文消息
    
    
    创建标识：Senparc - 20220503

----------------------------------------------------------------*/
using Senparc.NeuChar;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// article_id 按钮
    /// <para>用户点击 article_id 类型按钮后，微信客户端将会以卡片形式，下发开发者在按钮中填写的图文消息</para>
    /// </summary>
    public class SingleArticleIdButton : SingleButton
    {
        /// <summary>
        /// 发布后获得的合法 article_id
        /// </summary>
        public string article_id { get; set; }

        public SingleArticleIdButton()
            : base(MenuButtonType.article_id.ToString())
        {
        }
    }
}
