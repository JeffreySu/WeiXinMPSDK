/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：TempleteModel.cs
    文件功能描述：模板消息接口需要的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    public class TempleteModel
    {
        /// <summary>
        /// 目标用户OpenId
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 模板消息顶部颜色（16进制），默认为#FF0000
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        public string url { get; set; }


        public TempleteModel()
        {
            topcolor = "#FF0000";
        }
    }
}
