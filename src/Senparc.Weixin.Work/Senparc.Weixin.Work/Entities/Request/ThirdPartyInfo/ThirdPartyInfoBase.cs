/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ThirdPartyInfoBase.cs
    文件功能描述：第三方应用授权回调消息服务
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    public interface IThirdPartyInfoBase : IWorkRequestMessageBase
    {
        ThirdPartyInfo InfoType { get; }
        string SuiteId { get; set; }
        string TimeStamp { get; set; }
    }

    public abstract class ThirdPartyInfoBase : WorkRequestMessageBase, IThirdPartyInfoBase
    {
        #region 以下内容为第三方应用授权回调消息服务
        public virtual ThirdPartyInfo InfoType
        {
            get { return ThirdPartyInfo.Unkonwn; }
        }

        /// <summary>
        /// 应用套件的SuiteId
        /// </summary>
        public string SuiteId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
        #endregion

        protected ThirdPartyInfoBase()
            : base()
        {

        }
    }
}
