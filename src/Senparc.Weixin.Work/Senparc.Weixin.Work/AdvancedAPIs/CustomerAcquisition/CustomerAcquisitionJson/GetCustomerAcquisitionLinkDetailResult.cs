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

    文件名：GetCustomerAcquisitionLinkDetailResult.cs
    文件功能描述：获客链接详情返回参数

    创建标识：IcedMango - 20240809

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson
{
    /// <summary>
    ///     获客链接详情返回参数
    /// </summary>
    public class GetCustomerAcquisitionLinkDetailResult : WorkJsonResult
    {
        /// <summary>
        ///     获客链接信息
        /// </summary>
        public CustomerAcquisitionLinkInfo link { get; set; }

        /// <summary>
        ///     获客链接使用范围
        /// </summary>
        public CustomerAcquisitionRange range { get; set; }
        
        /// <summary>
        ///     获客链接_优先分配类型
        /// </summary>
        public CustomerAcquisitionPriority priority_option { get; set; }
    }

    /// <summary>
    ///     获客链接信息
    /// </summary>
    public class CustomerAcquisitionLinkInfo
    {
        /// <summary>
        ///     获客链接名称
        /// </summary>
        public string link_name { get; set; }

        /// <summary>
        ///     获客链接(https://work.weixin.qq.com/ca/xxxxxx)
        /// </summary>
        public string url { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public long? create_time { get; set; }

        /// <summary>
        ///     是否无需验证
        /// </summary>
        public bool? skip_verify { get; set; }
    }
    
    /// <summary>
    ///     获客链接范围
    /// </summary>
    public class CustomerAcquisitionRange
    {
        /// <summary>
        ///     该获客链接使用范围成员列表
        /// </summary>
        public List<string> user_list { get; set; }
        
        /// <summary>
        ///     该获客链接使用范围的部门列表
        /// </summary>
        public List<long> department_list { get; set; }
    }
    
    /// <summary>
    ///     获客链接_优先分配类型
    /// </summary>
    public class CustomerAcquisitionPriority
    {
        /// <summary>
        ///     优先分配类型，1-全企业范围内优先分配给有好友关系的；2-指定范围内优先分配有好友关系的
        /// </summary>
        public int? priority_type { get; set; }
        
        /// <summary>
        ///     priority_type为2时的指定成员列表
        /// </summary>
        public List<string> priority_userid_list { get; set; }
    }
}