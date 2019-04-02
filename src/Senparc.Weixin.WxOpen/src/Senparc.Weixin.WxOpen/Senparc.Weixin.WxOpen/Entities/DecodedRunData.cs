using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DecodedRunData
    文件功能描述：小程序运动步数解密类
    
    
    创建标识：2019-04-02
----------------------------------------------------------------*/
namespace Senparc.Weixin.WxOpen.Entities
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