/*----------------------------------------------------------------
    Copyright (C) 2023 Senparc
    
    文件名：CreateIcpVerifyTaskResultJson.cs
    文件功能描述：GetOnlineIcpOrderResultJson 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 获取小程序已备案详情
    /// </summary>
    public class GetOnlineIcpOrderResultJson
    {
        /// <summary>
        /// 备案主体信息，不包括图片、视频材料(参考：申请小程序备案接口的 `ICPSubject`)
        /// </summary>
        public IcpSubjectModel icp_subject { get; set; }

        /// <summary>
        /// 微信小程序信息，不包括图片、视频材料(参考：申请小程序备案接口的 `ICPApplets`)
        /// </summary>
        public List<IcpAppletsModel> icp_applets { get; set; }
    }
}
