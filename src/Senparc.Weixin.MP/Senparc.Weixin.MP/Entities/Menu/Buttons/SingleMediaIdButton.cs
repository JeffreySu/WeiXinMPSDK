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
    
    文件名：SingleMediaIdButton.cs
    文件功能描述：下发消息（除文本消息）按钮
    
    
    创建标识：Senparc - 20170824

    修改标识：Senparc - 20181005
    修改描述：菜单按钮类型（ButtonType）改为使用 Senparc.NeuChar.MenuButtonType
    
----------------------------------------------------------------*/
using Senparc.NeuChar;
using System;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 下发消息（除文本消息）按钮
    /// </summary>
    [Obsolete("草稿接口灰度完成后，将不再支持图文信息类型的 media_id 和 view_limited，有需要的，请使用 article_id 和 article_view_limited 代替")]
    public class SingleMediaIdButton : SingleButton
    {
        /// <summary>
        /// 下发消息（除文本消息）用户点击media_id类型按钮后，微信服务器会将开发者填写的永久素材id对应的素材下发给用户，永久素材类型可以是图片、音频、视频、图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
        /// </summary>
        public string media_id { get; set; }

        public SingleMediaIdButton()
            : base(MenuButtonType.media_id.ToString())
        {
        }
    }
}
