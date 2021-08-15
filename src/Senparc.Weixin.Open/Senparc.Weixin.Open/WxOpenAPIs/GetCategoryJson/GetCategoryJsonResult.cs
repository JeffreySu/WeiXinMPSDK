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

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：AddJsonResult.cs
    文件功能描述：“获取小程序账号的类目”接口：Get 结果
    
    
    创建标识：ccccccmd - 20210302

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxOpenAPIs.GetCategoryJson
{
    /// <summary>
    /// 账号已经设置的所有类目
    /// </summary>
    [Serializable]
    public class GetCategoryJsonResult : WxJsonResult
    {
        public GetCategoryJsonResult()
        {
            categories = new List<Category>();
        }
        /// <summary>
        /// 一个更改周期内可以设置类目的次数
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 本更改周期内还可以设置类目的次数
        /// </summary>
        public int quota { get; set; }

        /// <summary>
        /// 最多可以设置的类目数量
        /// </summary>
        public int category_limit { get; set; }

        public List<Category> categories { get; set; }


    }

    public class Category
    {
        /// <summary>
        /// 一级类目ID
        /// </summary>
        public int first { get; set; }

        /// <summary>
        /// 一级类目名称
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// 二级类目ID
        /// </summary>
        public int second { get; set; }

        /// <summary>
        /// 二级类目名称
        /// </summary>
        public string second_name { get; set; }

        /// <summary>
        /// 审核状态（1审核中 2审核不通过 3审核通过）
        /// </summary>
        public AuditStatus audit_status { get; set; }

        /// <summary>
        /// 审核不通过原因
        /// </summary>
        public string audit_reason { get; set; }
    }
}