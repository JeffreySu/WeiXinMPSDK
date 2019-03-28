/*----------------------------------------------------------------
  
    文件名：DecodedRunData.cs
    文件功能描述：用户运动步数解密类
    
    
    创建标识：2019-03-28
----------------------------------------------------------------*/

using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senparc.Weixin.MP.Sample.Models
{
    //  "stepInfoList": [
    //  {
    //    "timestamp": 1445866601,
    //    "step": 100
    //  },
    //  {
    //    "timestamp": 1445876601,
    //    "step": 120
    //  }
    //]
    [Serializable]
    public class DecodedRunData : DecodeEntityBase
    {
        public List<stepModel> stepInfoList { get; set; }
    }

    public class stepModel
    {
        public long timestamp { get; set; }
        public long step { get; set; }
    }
}