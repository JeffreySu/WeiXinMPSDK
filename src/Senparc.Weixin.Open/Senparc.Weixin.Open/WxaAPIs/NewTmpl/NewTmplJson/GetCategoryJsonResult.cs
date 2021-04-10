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

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxaAPIs.NewTmpl.NewTmplJson
{
    public class GetCategoryJsonResult : WxJsonResult
    {

        /// <summary>
        /// 	类目列表
        /// </summary>
        public List<GetCategoryJsonResultt_data> data { get; set; }
    }

    public class GetCategoryJsonResultt_data
    {
        /// <summary>
        /// 类目id，查询公共模板库时需要
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 类目的中文名
        /// </summary>
        public string name { get; set; }
    }
}