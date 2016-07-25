/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BaseSemanticResult.cs
    文件功能描述：语意理解接口基础返回信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Semantic
{
    public class BaseSemanticResultJson : WxJsonResult
    {
        /// <summary>
        /// 用于标识用户请求后的状态
        /// 文档中写的是res，但实际测试应该是ret
        /// </summary>
        public string ret { get; set; }
        /// <summary>
        /// 用户的输入字符串
        /// </summary>
        public string query { get; set; }
        /// <summary>
        /// 服务的全局类别id
        /// </summary>
        public string type { get; set; }
    }

    public class BaseSemanticIntent
    {
        /// <summary>
        /// SEARCH 普通查询
        /// ROUTE 路线查询
        /// </summary>
        public string intent { get; set; }
    }
}
