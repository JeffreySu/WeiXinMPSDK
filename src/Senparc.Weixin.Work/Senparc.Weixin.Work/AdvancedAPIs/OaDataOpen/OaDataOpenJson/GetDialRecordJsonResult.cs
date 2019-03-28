/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetDialRecordJsonResult.cs
    文件功能描述：获取公费电话拨打记录返回结果
    
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    /// <summary>
    /// 获取公费电话拨打记录返回结果
    /// </summary>
    public class GetDialRecordJsonResult : WorkJsonResult
    {
        public Record[] record { get; set; }
    }

    public class Record
    {
        public int call_time { get; set; }
        public int total_duration { get; set; }
        public int call_type { get; set; }
        public Caller caller { get; set; }
        public object callee { get; set; }
        public int calltime { get; set; }
    }

    public class Caller
    {
        public string userid { get; set; }
        public int duration { get; set; }
    }


}
