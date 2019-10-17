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
    
    文件名：WxDatabaseJsonResult.cs
    文件功能描述：数据库操作记录的各类返回结果
    
    
    创建标识：lishewen - 20190530
   
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 数据库插入记录 返回结果
    /// </summary>
    public class WxDatabaseAddJsonResult : WxJsonResult
    {
        /// <summary>
        /// 插入成功的数据集合主键_id。
        /// </summary>
        public string[] id_list { get; set; }
    }

    /// <summary>
    /// 数据库删除记录 返回结果
    /// </summary>
    public class WxDatabaseDeleteJsonResult : WxJsonResult
    {
        /// <summary>
        /// 删除记录数量
        /// </summary>
        public int deleted { get; set; }
    }

    /// <summary>
    /// 数据库更新记录 返回结果
    /// </summary>
    public class WxDatabaseUpdateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 更新条件匹配到的结果数
        /// </summary>
        public int matched { get; set; }
        /// <summary>
        /// 修改的记录数，注意：使用set操作新插入的数据不计入修改数目
        /// </summary>
        public int modified { get; set; }
        /// <summary>
        /// 新插入记录的id，注意：只有使用set操作新插入数据时这个字段会有值
        /// </summary>
        public string id { get; set; }
    }

    /// <summary>
    /// 数据库查询记录 返回结果
    /// </summary>
    public class WxDatabaseQueryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pager pager { get; set; }
        /// <summary>
        /// 记录数组
        /// </summary>
        public string[] data { get; set; }
    }

    /// <summary>
    /// 统计集合记录数或统计查询语句对应的结果记录数 返回结果
    /// </summary>
    public class WxDatabaseCountJsonResult : WxJsonResult
    {
        /// <summary>
        /// 记录数量
        /// </summary>
        public int count { get; set; }
    }
}
