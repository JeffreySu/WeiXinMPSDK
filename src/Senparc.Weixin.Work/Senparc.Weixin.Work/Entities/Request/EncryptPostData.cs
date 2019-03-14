/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：EncryptPostData.cs
    文件功能描述：原始加密信息
    
    
    创建标识：Senparc - 20150313
 
    修改标识：Senparc - 20160802
    修改描述：将其AgentID类型改为int?
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    public class EncryptPostData
    {
        public string ToUserName { get; set; }
        public string Encrypt { get; set; }
        public int? AgentID { get; set; }
    }
}
