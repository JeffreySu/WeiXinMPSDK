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

    文件名：GetMsgListResultJson.cs
    文件功能描述：GetMsgListResultJson


    创建标识：Senparc - 20160725

    修改标识：Senparc - 20170522
    修改描述：v16.6.2 修改 DateTime 为 DateTimeOffset

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    public class GetMsgListResultJson : WxJsonResult
    {
        public List<GetMsgList> recordList { get; set; }
        public int number { get; set; }
        public long msgid { get; set; }
    }

    public class GetMsgList
    {
        public string openid { get; set; }
        public string opercode { get; set; }
        public string text { get; set; }
        public DateTimeOffset time { get; set; }
        public string worker { get; set; }
    }
}
