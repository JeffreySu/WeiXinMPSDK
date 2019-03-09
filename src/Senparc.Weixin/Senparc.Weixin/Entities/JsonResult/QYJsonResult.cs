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

    文件名：QyJsonResult.cs
    文件功能描述：企业号JSON返回结果


    创建标识：Senparc - 20150928

    修改标识：Senparc - 20160810
    修改描述：v4.1.4 QyJsonResult添加序列化标签

    修改标识：Senparc - 20170702
    修改描述：v4.13.0 添加 ErrorCodeValue 属性。使用 BaseJsonResult 基类。

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 企业号 JSON 返回结果
    /// </summary>
    [Serializable]
    public class QyJsonResult : BaseJsonResult
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        public ReturnCode_QY errcode { get; set; }

        /// <summary>
        /// 返回消息代码数字（同errcode枚举值）
        /// </summary>
        public override int ErrorCodeValue { get { return (int)errcode; } }
    }
}
