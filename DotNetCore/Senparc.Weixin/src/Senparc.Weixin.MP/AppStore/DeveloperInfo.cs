/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：ReturnResult.cs
    文件功能描述：微微嗨开发者信息
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore
{
    /// <summary>
    /// 微微嗨开发者信息。申请开发者：http://www.weiweihi.com/User/Developer/Apply
    /// </summary>
    public class DeveloperInfo
    {
        ///// <summary>
        ///// 微微嗨开发者的AppKey，可以在 http://www.weiweihi.com/Developer/Developer 找到
        ///// </summary>
        //public string AppKey { get; set; }

        ///// <summary>
        ///// 微微嗨开发者的Secret，可以在 http://www.weiweihi.com/Developer/Developer 找到
        ///// </summary>
        //public string AppSecret { get; set; }

        /// <summary>
        /// 在www.weiweihi.com对接微信公众号之后，自动生成的WeiweihiKey。
        /// </summary>
        public string WeiweihiKey { get; set; }
    }
}
