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
    
    文件名：CodeResultJson.cs
    文件功能描述：代码管理返回结果
    
    
    创建标识：Senparc - 20170726

    修改标识：Senparc - 20220918
    修改描述：v4.14.10 “小程序版本回退”接口更新返回参数内容
----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class CodeResultJson : WxJsonResult
    {
        /// <summary>
        /// 模板信息列表
        /// </summary>
        public Version_List[] version_list { get; set; }
    }

    public class Version_List
    {
        /// <summary>
        /// 更新时间，时间戳
        /// </summary>
        public long commit_time { get; set; }
        /// <summary>
        /// 模板版本号，开发者自定义字段
        /// </summary>
        public string user_version { get; set; }
        /// <summary>
        /// 模板描述，开发者自定义字段
        /// </summary>
        public string user_desc { get; set; }
        /// <summary>
        /// 小程序版本
        /// </summary>
        public int app_version { get; set; }
    }

}
