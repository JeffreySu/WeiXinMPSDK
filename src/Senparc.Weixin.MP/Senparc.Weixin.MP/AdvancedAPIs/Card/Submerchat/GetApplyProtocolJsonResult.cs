#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：GetApplyProtocolJsonResult.cs
    文件功能描述：卡券开放类目查询的返回结果
    
    
    创建标识：Senparc - 20160520
    
    修改标识：Senparc - 20160520
    修改描述：整理接口
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 卡券开放类目查询的返回结果
    /// </summary>
    public class GetApplyProtocolJsonResult : WxJsonResult
    {
        public List<GetApplyProtocol_Category> category { get; set; }
    }

    public class GetApplyProtocol_Category
    {
        /// <summary>
        /// 一级目录id
        /// </summary>
        public int primary_category_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string category_name { get; set; }
        /// <summary>
        /// 二级目录id
        /// </summary>
        public List<GetApplyProtocol_Secondary_Category> secondary_category { get; set; }


    }
    public class GetApplyProtocol_Secondary_Category
    {
        /// <summary>
        /// secondary_category_id
        /// </summary>
        public int secondary_category_id { get; set; }
        /// <summary>
        /// category_name
        /// </summary>
        public string category_name { get; set; }
        /// <summary>
        /// need_qualification_stuffs
        /// </summary>
        public List<string> need_qualification_stuffs { get; set; }
        /// <summary>
        /// can_choose_prepaid_card
        /// </summary>
        public int can_choose_prepaid_card { get; set; }
        /// <summary>
        /// can_choose_payment_card
        /// </summary>
        public int can_choose_payment_card { get; set; }
    }
}
