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

    文件名：IJsonResultCallback.cs
    文件功能描述：JSON数据（序列化）回调接口


    创建标识：Senparc - 20161209

----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// JSON 数据（序列化）回调接口
    /// </summary>
    public interface IJsonResultCallback
    {
        /// <summary>
        /// 序列化前回调
        /// </summary>
        void SerializingCallback();
        /// <summary>
        /// 序列化后回调
        /// </summary>
        /// <param name="json"></param>
        void SrializedCallback(string json);
        /// <summary>
        /// 反序列化前回调
        /// </summary>
        /// <param name="json"></param>
        void DeserializingCallback(string json);
        /// <summary>
        /// 反序列化后回调
        /// </summary>
        /// <param name="json"></param>
        void DeserializedCallback(string json);
    }

}
