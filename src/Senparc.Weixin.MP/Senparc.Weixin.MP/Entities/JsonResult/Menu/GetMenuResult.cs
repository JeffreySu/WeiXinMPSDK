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
    
    文件名：GetMenuResult.cs
    文件功能描述：获取菜单返回的Json结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20190606
    修改描述：v6.4.8 为兼容新版本的 TryCommonApiBase<T>，添加不带参数的构造函数
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// GetMenu返回的Json结果
    /// </summary>
    public class GetMenuResult : WxJsonResult
    {
        //TODO：这里如果有更加复杂的情况，可以换成ButtonGroupBase类型，并提供泛型
        public ButtonGroupBase menu { get; set; }

        /// <summary>
        /// 有个性化菜单时显示。最新的在最前。
        /// </summary>
        public List<ConditionalButtonGroup> conditionalmenu { get; set; }

        public GetMenuResult(ButtonGroupBase buttonGroupBase)
        {
            menu = buttonGroupBase;
        }

        /// <summary>
        /// 请勿使用此构造函数创建对象
        /// </summary>
        public GetMenuResult() { }
    }
}
