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

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleMiniProgramButton.cs
    文件功能描述：小程序按钮
    
    
    创建标识：Senparc - 20170327

    修改标识：Senparc - 20181005
    修改描述：菜单按钮类型（ButtonType）改为使用 Senparc.NeuChar.MenuButtonType

----------------------------------------------------------------*/
using Senparc.NeuChar;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 小程序按钮
    /// </summary>
    public class SingleMiniProgramButton : SingleButton
    {
        /// <summary>
        /// 类型为miniprogram时必须
        /// 小程序Url，用户点击按钮可打开小程序，不超过1024字节（不支持小程序的老版本客户端将打开本url）
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 小程序的appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 小程序的页面路径
        /// </summary>
        public string pagepath { get; set; }

        public SingleMiniProgramButton()
            : base(MenuButtonType.miniprogram.ToString())
        {
        }
    }
}
