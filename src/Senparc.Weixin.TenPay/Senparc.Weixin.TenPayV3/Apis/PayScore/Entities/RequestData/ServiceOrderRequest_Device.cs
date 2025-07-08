using System;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{

    public class ServiceOrderRequest_Device
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="start_device_id">服务开始的设备ID  <para>某一设备在商户对应服务ID下的唯一标识，由商户自行填写，建议采用设备SN值。售货机、充电宝、充电桩等无人自助设备行业必传。</para></param>
        /// <param name="end_device_id">服务结束的设备ID  <para>某一设备在商户对应服务ID下的唯一标识，由商户自行填写，建议采用设备SN值。售货机、充电宝、充电桩等无人自助设备行业必传。</para></param>
        /// <param name="materiel_no">物料编码  <para>若商家参与政策，则商家填写行业侧给到商家的物料码（字母+数字的形式）；若商家未参与政策，则商家填写URL链接。</para></param>
        public ServiceOrderRequest_Device(string start_device_id, string end_device_id, string materiel_no)
        {
            this.start_device_id = start_device_id;
            this.end_device_id = end_device_id;
            this.materiel_no = materiel_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ServiceOrderRequest_Device()
        {
        }

        /// <summary>
        /// 服务开始的设备ID 
        /// <para>某一设备在商户对应服务ID下的唯一标识，由商户自行填写，建议采用设备SN值。售货机、充电宝、充电桩等无人自助设备行业必传。</para>
        /// </summary>
        public string start_device_id { get; set; }

        /// <summary>
        /// 服务结束的设备ID 
        /// <para> 某一设备在商户对应服务ID下的唯一标识，由商户自行填写，建议采用设备SN值。售货机、充电宝、充电桩等无人自助设备行业必传。</para>
        /// </summary>
        public string end_device_id { get; set; }

        /// <summary>
        /// 物料编码 
        /// <para>若商家参与政策，则商家填写行业侧给到商家的物料码（字母+数字的形式）；若商家未参与政策，则商家填写URL链接。</para>
        /// </summary>
        public string materiel_no { get; set; }
    }
}
