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
    
    文件名：SingleArticleViewLimitedButton.cs
    文件功能描述：类似 view_limited，但不使用 media_id 而使用 article_id：
                      跳转图文消息URL用户点击view_limited类型按钮后，
                      微信客户端将打开开发者在按钮中填写的永久素材id对应的图文消息URL，
                      永久素材类型只支持图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”
                      接口上传后获得的合法id。
    
    
    创建标识：Senparc - 20220503

----------------------------------------------------------------*/
using Senparc.NeuChar;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// SingleArticleViewLimitedButton 按钮
    /// <para>类似 view_limited，但不使用 media_id 而使用 article_id</para>
    /// </summary>
    public class SingleArticleViewLimitedButton : SingleButton
    {
        /// <summary>
        /// 类似 view_limited，但不使用 media_id 而使用 article_id
        /// </summary>
        public string article_id { get; set; }

        public SingleArticleViewLimitedButton()
            : base(MenuButtonType.article_view_limited.ToString())
        {
        }
    }
}
