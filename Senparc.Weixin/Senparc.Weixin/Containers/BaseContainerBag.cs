/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：BaseContainerBag.cs
    文件功能描述：微信容器接口中的封装Value（如Ticket、AccessToken等数据集合）
    
    
    创建标识：Senparc - 20151003
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.Containers
{
    /// <summary>
    /// IBaseContainerBag
    /// </summary>
    public interface IBaseContainerBag
    {
        string Key { get; set; }
    }

    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
    public class BaseContainerBag : IBaseContainerBag
    {
        /// <summary>
        /// 通常为AppId
        /// </summary>
        public string Key { get; set; }
    }

}
