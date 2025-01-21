#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc

    文件名：ModifyCustomerAcquisitionLinkRequest.cs
    文件功能描述：编辑获客链接参数

    创建标识：IcedMango - 20240809

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson
{
    /// <summary>
    ///     编辑获客链接
    /// </summary>
    public class ModifyCustomerAcquisitionLinkRequest
    {
        /// <summary>
        ///     获客链接名称
        /// </summary>
        public string link_id { get; set; }
        
        /// <summary>
        ///     获客链接名称
        /// </summary>
        public string link_name { get; set; }

        /// <summary>
        ///     获客链接使用范围
        /// </summary>
        public CustomerAcquisitionRange range { get; set; }

        /// <summary>
        ///     是否无需验证，默认为true
        /// </summary>
        public bool? skip_verify { get; set; }

        /// <summary>
        ///     获客链接_优先分配类型
        /// </summary>
        public CustomerAcquisitionPriority AcquisitionPriority { get; set; }
    }
}