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
    
    文件名：SubmerChantBatchGetJsonResult.cs
    文件功能描述：批量拉取子商户信息的返回结果
    
    
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
    /// 批量拉取子商户信息的返回结果
    /// </summary>
   public class SubmerChantBatchGetJsonResult : SubmerChantSubmitJsonResult
    {
      public List<SubmerChantBatchGet_InfoList> info_list { get; set; }
     }
   public class SubmerChantBatchGet_InfoList
   {
       public List<SubmerChantSubmit_info> info { get; set; }
       /// <summary>
       /// 拉渠道列表中最后一个子商户的id
       /// </summary>
       public int next_begin_id { get; set; }
   }
}
