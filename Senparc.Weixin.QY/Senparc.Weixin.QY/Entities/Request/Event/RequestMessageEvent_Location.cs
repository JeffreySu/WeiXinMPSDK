using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class RequestMessageEvent_Location : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.LOCATION; }
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }
    }
}
