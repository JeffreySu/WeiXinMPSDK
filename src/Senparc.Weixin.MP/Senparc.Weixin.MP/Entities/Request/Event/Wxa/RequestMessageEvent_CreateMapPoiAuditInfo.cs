namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_CreateMapPoiAuditInfo : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.create_map_poi_audit_info; }
        }

        /// <summary>
        /// 1.审核通过
        /// 2.审核中
        /// 3.审核失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 审核单id
        /// </summary>
        public long audit_id { get; set; }
        /// <summary>
        /// 从腾讯地图换取的位置点id
        /// </summary>
        public string map_poi_id { get; set; }
        /// <summary>
        /// 门店名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string sh_remark { get; set; }
    }
}
