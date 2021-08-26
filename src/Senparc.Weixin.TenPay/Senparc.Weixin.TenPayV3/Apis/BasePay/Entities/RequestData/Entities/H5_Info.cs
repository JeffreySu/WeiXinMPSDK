#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：H5_Info.cs
    文件功能描述：下单请求H5场景信息
    
    
    创建标识：Senparc - 20210825
    
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities.RequestData.Entities
{

    /// <summary>
    /// H5场景信息
    /// </summary>
    public class H5_Info
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="type">场景类型</param>
        /// <param name="app_name">应用名称，可为null</param>
        /// <param name="app_url">应用URL，可为null</param>
        /// <param name="bundle_id">iOS平台BundleID，可为null</param>
        /// <param name="package_name">Android平台PackageName，可为null</param>
        public H5_Info(string type, string app_name, string app_url, string bundle_id, string package_name)
        {
            this.type = type;
            this.app_name = app_name;
            this.app_url = app_url;
            this.bundle_id = bundle_id;
            this.package_name = package_name;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public H5_Info()
        {
        }

        /// <summary>
        /// 场景类型
        /// 示例值：iOS, Android, Wap
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 应用名称
        /// 示例值：王者荣耀
        /// </summary>
        public string app_name { get; set; }

        /// <summary>
        /// 网站URL
        /// 示例值：https://pay.qq.com
        /// </summary>
        public string app_url { get; set; }

        /// <summary>
        /// iOS平台BundleID
        /// 示例值：com.tencent.wzryiOS
        /// </summary>
        public string bundle_id { get; set; }

        /// <summary>
        /// Android平台PackageName
        /// 示例值：com.tencent.tmgp.sgame
        /// </summary>
        public string package_name { get; set; }
    }
}
