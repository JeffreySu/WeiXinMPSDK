﻿#region Apache License Version 2.0
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
    
    文件名：CodeResultJson.cs
    文件功能描述：Code相关接口返回结果
    
    
    创建标识：Senparc - 20150907
----------------------------------------------------------------*/


using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 查询导入code数目返回结果
    /// </summary>
    public class GetDepositCountResultJson : WxJsonResult
    {
        /// <summary>
        /// 货架已经成功存入的code数目。
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 核查code返回结果
    /// </summary>
    public class CheckCodeResultJson : WxJsonResult
    {
        /// <summary>
        /// 已经成功存入的code。
        /// </summary>
        public List<string> exist_code { get; set; }
        /// <summary>
        /// 没有存入的code。
        /// </summary>
        public List<string> not_exist_code { get; set; }
    }
}
