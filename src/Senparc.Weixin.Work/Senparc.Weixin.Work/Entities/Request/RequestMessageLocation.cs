/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageLocation.cs
    文件功能描述：接收普通地理位置消息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public class RequestMessageLocation : WorkRequestMessageBase, IRequestMessageLocation
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
