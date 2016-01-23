using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Senparc.Weixin.MP.Sample.Models
{
    class Fhd
    {
        /// <summary>
        /// 单据ID号
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 发送人的微信号
        /// </summary>
        public string WeixinID { get; set; }

        /// <summary>
        /// 发送者所在经度
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// 发送者所在纬度
        /// </summary>
        public decimal latitude { get; set; }

        /// <summary>
        /// 发货人姓名
        /// </summary>
        public string SendName { get; set; }

        /// <summary>
        /// 发货人电话
        /// </summary>
        public string SendPhone { get; set; }

        /// <summary>
        /// 发货人电话
        /// </summary>
        public string SendAddress { get; set; }

        /// <summary>
        /// 发货人身份证号
        /// </summary>
        public string SendID { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 收货人身份证号
        /// </summary>
        public string ReceiverID { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        public string HwName { get; set; }

        /// <summary>
        /// 货物数量
        /// </summary>
        public string HwQuantity { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal Shipment { get; set; }

        /// <summary>
        /// 代收货款
        /// </summary>
        public decimal Money { get; set; }
    }
}
