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
  
    文件名：SmartGuideRequestData.cs
    文件功能描述：智能导购 - API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.SmartGuide
{
    /// <summary>
    /// 智能导购 - 服务人员注册API 请求数据
    /// </summary>
    public class RegisterSmartGuideRequestData
    {
        public string corp_id { get; set; }
        public string store_id { get; set; }
        public string userid { get; set; }
        public string mobile { get; set; }
        public string work_id { get; set; }
        public string name { get; set; }
        public string qr_code { get; set; }
        public string avatar { get; set; }
        public string group_qrcode { get; set; }
    }

    /// <summary>
    /// 智能导购 - 服务人员分配API 请求数据
    /// </summary>
    public class AssignSmartGuideRequestData
    {
        public string guide_id { get; set; }
        public string out_trade_no { get; set; }
    }

    /// <summary>
    /// 智能导购 - 服务人员查询API 请求数据
    /// </summary>
    public class QuerySmartGuideRequestData
    {
        public string store_id { get; set; }
        public string userid { get; set; }
        public string mobile { get; set; }
        public string work_id { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }

    /// <summary>
    /// 智能导购 - 服务人员信息更新API 请求数据
    /// </summary>
    public class UpdateSmartGuideRequestData
    {
        public string guide_id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string qr_code { get; set; }
        public string avatar { get; set; }
        public string group_qrcode { get; set; }
    }
}
