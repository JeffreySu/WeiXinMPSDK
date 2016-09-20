/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：PostModel.cs
    文件功能描述：微信公众服务器Post过来的加密参数集合（不包括PostData）
    
    
    创建标识：Senparc - 201500712
 
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open.Entities.Request
{
    /// <summary>
    /// 微信公众服务器Post过来的加密参数集合（不包括PostData）
    /// </summary>
    public class PostModel : EncryptPostModel
    {
        /// <summary>
        /// 开发平台“公众号第三方平台”的AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 加密类型，通常为"aes"
        /// </summary>
        public string Encrypt_Type { get; set; }
    }
}
