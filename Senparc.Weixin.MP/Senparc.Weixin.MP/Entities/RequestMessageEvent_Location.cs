using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Location : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 地理位置维度，事件类型为LOCATION的时存在
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度，事件类型为LOCATION的时存在
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度，事件类型为LOCATION的时存在
        /// </summary>
        public double Precision { get; set; }
    }
}
