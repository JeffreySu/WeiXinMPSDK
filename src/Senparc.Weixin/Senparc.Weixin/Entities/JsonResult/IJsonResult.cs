#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.Threading.Tasks;

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
        object P2PData { get; set; }
    }

    /// <summary>
    /// 包含 errorcode 的 Json 返回结果接口
    /// </summary>
    public interface IWxJsonResult : IJsonResult
    {
        /// <summary>
        /// 返回结果代码
        /// </summary>
        ReturnCode errcode { get; set; }
    }
}
