#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：ApplySetOrderPathInfo.cs
    文件功能描述：申请设置订单页 path 信息
    
    
    创建标识：mojinxun - 20230207

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 申请设置订单页 path 信息
    /// </summary>
    public class ApplySetOrderPathInfo
    {
        /// <summary>
        /// 订单页path
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 申请提交的图片url，审核版会显示
        /// </summary>
        public List<string> img_list { get; set; }

        /// <summary>
        /// 申请提交的视频url，审核版会显示
        /// </summary>
        public string video { get; set; }

        /// <summary>
        /// 申请提交的测试账号，审核版会显示
        /// </summary>
        public string test_account { get; set; }

        /// <summary>
        /// 申请提交的测试密码，审核版会显示
        /// </summary>
        public string test_pwd { get; set; }

        /// <summary>
        /// 申请提交的测试备注，审核版会显示
        /// </summary>
        public string test_remark { get; set; }

        /// <summary>
        /// 申请提交的批量的appid
        /// </summary>
        public List<string> appid_list { get; set; }
    }
}
