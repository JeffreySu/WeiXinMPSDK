/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ConvertToOpenIdResult.cs
    文件功能描述：userid转换成openid接口返回的Json结果
    
    
    创建标识：Senparc - 20150703
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// userid转换成openid接口返回的Json结果
    /// </summary>
    public class ConvertToOpenIdResult : QyJsonResult
    {
        /// <summary>
        /// 企业号成员userid对应的openid，若有传参agentid，则是针对该agentid的openid。否则是针对企业号corpid的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 应用的appid，若请求包中不包含agentid则不返回appid。该appid在使用微信红包时会用到
        /// </summary>
        public string appid { get; set; }
    }
}
