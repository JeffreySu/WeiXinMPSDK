﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
    
    文件名：MerchantInfoGetResultJson.cs
    文件功能描述：获取商户信息的返回结果
    
    
    创建标识：Senparc - 20160520
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Scan
{
    /// <summary>
    /// 获取商户信息的返回结果
    /// </summary>
    public class MerchantInfoGetResultJson : WxJsonResult 
    {
        /// <summary>
        /// 品牌标签列表，创建商品时传入，商户自定义生成的品牌标识字段。
        /// </summary>
        public List<string> brand_tag_list { get; set; }
        /// <summary>
        /// 权限列表，包含商户号段、类目ID、类目名称三者的对应关系。
        /// </summary>
        public List<MerchantInfoGet_Verified_List> verified_list { get; set; }

        
    }
    public class MerchantInfoGet_Verified_List
    {
        /// <summary>
        /// 商户号段，表示该商户下有资质的条码号段。
        /// </summary>
        public string verified_firm_code { get; set; }
        /// <summary>
        /// 商户类目列表，包含类目ID与对应的类目名称。
        /// </summary>
        public List<MerchantInfoGet_Verified_Cate_List> verified_cate_list { get; set; }





    }
    public class MerchantInfoGet_Verified_Cate_List
    {
        /// <summary>
        /// 商户类目ID，表示该商户下可用于创建商的类目ID
        /// </summary>
        public string verified_cate_id { get; set; }
        /// <summary>
        /// 商户类目名称，对应类目ID的名称
        /// </summary>
        public string verified_cate_name { get; set; }
    }
}
