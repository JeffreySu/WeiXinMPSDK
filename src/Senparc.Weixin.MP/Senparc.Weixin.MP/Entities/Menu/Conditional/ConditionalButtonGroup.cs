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
    
    文件名：ConditionalButtonGroup.cs
    文件功能描述：个性化菜单按钮设置（可以直接用ConditionalButtonGroup实例返回JSON对象）
    
    
    创建标识：Senparc - 20151222

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 个性化菜单按钮集合
    /// </summary>
    public class ConditionalButtonGroup :ButtonGroupBase, IButtonGroupBase
    {
        public MenuMatchRule matchrule { get; set; }
        /// <summary>
        /// 菜单Id，只在获取的时候自动填充，提交“菜单创建”请求时不需要设置
        /// </summary>
        public long menuid { get; set; }
    }
}
