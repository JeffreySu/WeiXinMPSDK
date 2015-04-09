/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ThirdPartyInfoBase.cs
    文件功能描述：第三方应用授权回调消息服务
    
    
    创建标识：Senparc - 20150401
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public interface IThirdPartyInfoBase
    {
        ThirdPartyInfo InfoType { get; }
        string AppId { get; set; }
        string CreateTime { get; set; }
    }

    public class ThirdPartyInfoBase : IThirdPartyInfoBase
    {
        #region 以下内容为第三方应用授权回调消息服务
        public virtual ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.component_verify_ticket; }
        }

        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string CreateTime { get; set; }
        #endregion

        public ThirdPartyInfoBase()
            : base()
        {

        }
    }
}
