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

    文件名：IJsonResult.cs
    文件功能描述：所有JSON返回结果基类


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20170702
    修改描述：v4.13.0
              1、将 IWxJsonResult 定义移入到 WxResult.cs 文件
              2、添加 ErrorCodeValue 只读属性

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 所有 JSON 格式返回值的API返回结果接口
    /// </summary>
    public interface IJsonResult// : IJsonResultCallback
    {
        /// <summary>
        /// 返回结果信息
        /// </summary>
        string errmsg { get; set; }

        /// <summary>
        /// errcode的
        /// </summary>
        int ErrorCodeValue { get; }
        object P2PData { get; set; }
    }
}
