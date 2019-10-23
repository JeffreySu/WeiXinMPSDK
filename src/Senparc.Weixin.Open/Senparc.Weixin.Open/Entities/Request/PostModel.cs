/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：PostModel.cs
    文件功能描述：微信公众服务器Post过来的加密参数集合（不包括PostData）
    
    
    创建标识：Senparc - 201500712
 
    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar

    修改标识：Senparc - 20181117
    修改描述：v4.2.0 添加 DomainId 属性


----------------------------------------------------------------*/

using Senparc.NeuChar;

namespace Senparc.Weixin.Open.Entities.Request
{
    /// <summary>
    /// 微信公众服务器Post过来的加密参数集合（不包括PostData）
    /// </summary>
    public class PostModel : EncryptPostModel
    {
        public override string DomainId { get => AppId; set => AppId = value; }

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
