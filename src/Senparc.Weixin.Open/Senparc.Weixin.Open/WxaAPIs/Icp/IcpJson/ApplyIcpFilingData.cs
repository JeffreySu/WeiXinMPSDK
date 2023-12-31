/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：ApplyIcpFilingData.cs
    文件功能描述：申请小程序备案 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 申请小程序备案 接口返回消息
    /// </summary>
    public class ApplyIcpFilingData
    {
        /// <summary>
        /// 备案主体信息
        /// </summary>
        public IcpSubjectModel icp_subject { get; set; }

        /// <summary>
        /// 微信小程序信息
        /// </summary>
        public IcpAppletsModel icp_applets { get; set; }

        /// <summary>
        /// 其他备案媒体材料
        /// </summary>
        public IcpMaterialsModel icp_materials { get; set; }
    }

}
