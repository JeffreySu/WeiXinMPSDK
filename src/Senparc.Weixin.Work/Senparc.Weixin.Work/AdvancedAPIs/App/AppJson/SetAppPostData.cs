/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SetAppPostData.cs
    文件功能描述：设置企业号应用需要Post的数据
    
    
    创建标识：Senparc - 20150316


    修改标识：Senparc - 20170703
    修改描述：增加IApiData接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.App
{
    /// <summary>
    /// 设置企业号应用需要Post的数据【QY移植修改】
    /// </summary>
    public class SetAppPostData //:IApiData
    {
        /// <summary>
        /// 企业应用id
        /// </summary>
        public string agentid { get; set; }
        /// <summary>
        /// 企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
        /// </summary>
        public string report_location_flag { get; set; }
        /// <summary>
        /// 企业应用头像的mediaid，通过多媒体接口上传图片获得mediaid，上传后会自动裁剪成方形和圆形两个头像
        /// </summary>
        public string logo_mediaid { get; set; }
        /// <summary>
        /// 企业应用名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 企业应用详情
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 企业应用可信域名
        /// </summary>
        public string redirect_domain { get; set; }
        /// <summary>
        /// 是否接收用户变更通知。0：不接收；1：接收
        /// </summary>
        public int isreportuser { get; set; }
        ///// <summary>
        ///// 是否上报用户进入应用事件。0：不接收；1：接收
        ///// </summary>
        //public int isreportenter { get; set; }

        /// <summary>
        /// 应用主页url。url必须以http或者https开头。
        /// </summary>
        public string home_url { get; set; }
    }
}
