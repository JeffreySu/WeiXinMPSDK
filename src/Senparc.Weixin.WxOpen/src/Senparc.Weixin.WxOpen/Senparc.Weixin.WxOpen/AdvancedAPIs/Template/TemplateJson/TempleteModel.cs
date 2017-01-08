/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：TempleteModel.cs
    文件功能描述：小程序模板消息接口需要的数据
    
    
    创建标识：Senparc - 20161112
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Template.TemplateJson
{
    /// <summary>
    /// 模板消息Post数据
    /// </summary>
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
        /// 点击模板查看详情跳转页面，不填则模板无跳转（非必填）
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// 表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id
        /// </summary>
        public string form_id { get; set; }


        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string emphasis_keyword { get; set; }



        public TempleteModel()
        {
        }
    }
}
