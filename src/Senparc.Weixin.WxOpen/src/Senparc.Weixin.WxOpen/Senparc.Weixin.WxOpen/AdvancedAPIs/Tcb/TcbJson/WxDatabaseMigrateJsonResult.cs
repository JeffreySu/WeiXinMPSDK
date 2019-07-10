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
    
    文件名：WxDatabaseMigrateJsonResult.cs
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
    /// 数据库导入/导出 返回结果
    /// </summary>
    public class WxDatabaseMigrateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 任务ID，可使用数据库迁移进度查询 API 查询进度及结果
        /// </summary>
        public int job_id { get; set; }
    }

    /// <summary>
    /// 数据库迁移状态查询 返回结果
    /// </summary>
    public class WxDatabaseMigrateQueryInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 导出状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 导出成功记录数
        /// </summary>
        public int record_success { get; set; }
        /// <summary>
        /// 导出失败记录数
        /// </summary>
        public int record_fail { get; set; }
        /// <summary>
        /// 导出错误信息
        /// </summary>
        public string err_msg { get; set; }
        /// <summary>
        /// 导出文件下载地址
        /// </summary>
        public string file_url { get; set; }
    }
}
