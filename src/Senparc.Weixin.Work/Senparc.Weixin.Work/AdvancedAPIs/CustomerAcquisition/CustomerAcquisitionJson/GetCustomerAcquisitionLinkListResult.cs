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

    文件名：GetCustomerAcquisitionLinkListResult.cs
    文件功能描述：获客链接列表返回参数

    创建标识：IcedMango - 20240809

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson;

/// <summary>
///     获客链接列表返回参数
/// </summary>
public class GetCustomerAcquisitionLinkListResult : WorkJsonResult
{
    /// <summary>
    /// 	link_id列表
    /// </summary>
    public List<string> link_id_list { get; set; }

    /// <summary>
    ///     分页游标
    /// </summary>
    public string next_cursor { get; set; }
}