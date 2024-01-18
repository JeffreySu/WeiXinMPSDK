/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：PostModel.cs
    文件功能描述：微信企业号服务器Post过来的参数集合（不包括PostData）
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20181117
    修改描述：v3.2.0 添加 DomainId 属性

----------------------------------------------------------------*/


using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// <para>微信企业号服务器Post过来的参数集合（不包括PostData）</para>
    /// <para>注意：企业号的 Url 信息中，验证信息为 Msg_Signature 字段，不是 Signature！</para>
    /// </summary>
    public class PostModel : EncryptPostModel
    {
        //TODO：这里使用 CorpId 并不合理，可能会造成重复，CorpId 是全局的，每个 App 会有不同的 AgentId
        public override string DomainId { get => CorpId; set => CorpId = value; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用
        public string CorpId { get; set; }

        public string CorpAgentId { get; set; }

        
    }
}
