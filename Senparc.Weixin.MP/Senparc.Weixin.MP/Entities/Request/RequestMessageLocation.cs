using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageLocation : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Location; }
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }
        public int Scale { get; set; }
        public string Label { get; set; }
    }
}
