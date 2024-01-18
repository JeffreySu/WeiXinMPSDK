#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：TemplateModel.cs
    文件功能描述：小程序模板消息接口需要的数据
    
    
    创建标识：Senparc - 20161112
    
    修改标识：Senparc - 20190906
    修改描述：v3.5.4 修正 UniformSendData 参数

    修改标识：Senparc - 20190906
    修改描述：v3.10.102 修正 UniformSendData.Mp_Template_Msg.Miniprogram  参数 pagepath -> page
                        反馈：https://weixin.senparc.com/QA-17333
    
    修改标识：Senparc - 20220120
    修改描述：v3.14.5 修改“下发小程序和公众号统一的服务消息”接口参数，提升兼容性

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Template
{
    /// <summary>
    /// 模板消息Post数据
    /// </summary>
    public class TemplateModel
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
        /// 模板需要放大的关键词，不填则默认无放大（非必填）
        /// </summary>
        public string emphasis_keyword { get; set; }

        /// <summary>
        /// 模板内容字体的颜色，不填默认黑色（非必填）
        /// </summary>
        public string color { get; set; }



        public TemplateModel()
        {
        }
    }

    /// <summary>
    /// 下发小程序和公众号统一的服务消息
    /// </summary>
    public class UniformSendData
    {
        /// <summary>
        /// （必须）用户openid，可以是小程序的openid，也可以是mp_template_msg.appid对应的公众号的openid
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 小程序模板消息相关的信息，可以参考小程序模板消息接口; 有此节点则优先发送小程序模板消息；（小程序模板消息已下线，不用传此节点）
        /// </summary>
        public Weapp_Template_Msg weapp_template_msg { get; set; }
        /// <summary>
        /// （必须）公众号模板消息相关的信息，可以参考公众号模板消息接口；有此节点并且没有weapp_template_msg节点时，发送公众号模板消息
        /// </summary>
        public Mp_Template_Msg mp_template_msg { get; set; }

        /// <summary>
        /// 下发小程序和公众号统一的服务消息
        /// </summary>
        /// <param name="touser">（必须）用户openid，可以是小程序的openid，也可以是mp_template_msg.appid对应的公众号的openid</param>
        /// <param name="mpTemplateMsg">（必须）公众号模板消息相关的信息，可以参考公众号模板消息接口；有此节点并且没有weapp_template_msg节点时，发送公众号模板消息</param>
        /// <param name="weappTemplateMsg">（可选）小程序模板消息相关的信息，可以参考小程序模板消息接口; 有此节点则优先发送小程序模板消息；（小程序模板消息已下线，不用传此节点）</param>
        public UniformSendData(string touser, Mp_Template_Msg mpTemplateMsg, Weapp_Template_Msg weappTemplateMsg = null)
        {
            this.touser = touser;
            this.mp_template_msg = mpTemplateMsg;
            this.weapp_template_msg = weappTemplateMsg;
        }
    }

    /// <summary>
    /// weappTemplateMsg
    /// </summary>
    public class Weapp_Template_Msg
    {
        /// <summary>
        /// 小程序模板ID
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 小程序页面路径
        /// </summary>
        public string page { get; set; }
        /// <summary>
        /// 小程序模板消息formid
        /// </summary>
        public string form_id { get; set; }
        /// <summary>
        /// 小程序模板数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 小程序模板放大关键词
        /// </summary>
        public string emphasis_keyword { get; set; }

        /// <summary>
        /// weappTemplateMsg
        /// </summary>
        /// <param name="templateId">小程序模板ID</param>
        /// <param name="page">小程序页面路径</param>
        /// <param name="formId">小程序模板消息formid</param>
        /// <param name="data">小程序模板数据</param>
        /// <param name="emphasisKeyword">小程序模板放大关键词</param>
        public Weapp_Template_Msg(string templateId, string page, string formId, object data, string emphasisKeyword)
        {
            this.template_id = templateId;
            this.page = page;
            this.form_id = formId;
            this.data = data;
            this.emphasis_keyword = emphasisKeyword;
        }
    }

    /// <summary>
    /// mpTemplateMsg
    /// </summary>
    public class Mp_Template_Msg
    {
        /// <summary>
        /// 公众号appid，要求与小程序有绑定且同主体
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 公众号模板id
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 公众号模板消息所要跳转的url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 公众号模板消息所要跳转的小程序，小程序的必须与公众号具有绑定关系，
        /// 可使用 <see cref="Miniprogram_Page"/> 或 <see cref="Miniprogram_PagePath"/>
        /// </summary>
        public IMiniprogram miniprogram { get; set; }
        /// <summary>
        /// 公众号模板消息的数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// mpTemplateMsg
        /// </summary>
        /// <param name="appid">公众号appid，要求与小程序有绑定且同主体</param>
        /// <param name="templateId">公众号模板id</param>
        /// <param name="url">公众号模板消息所要跳转的url</param>
        /// <param name="miniprogram">公众号模板消息所要跳转的小程序，小程序的必须与公众号具有绑定关系，可使用 <see cref="Miniprogram_Page"/> 或 <see cref="Miniprogram_PagePath"/></param>
        /// <param name="data">公众号模板消息的数据</param>
        public Mp_Template_Msg(string appid, string templateId, string url, IMiniprogram miniprogram, object data)
        {
            this.appid = appid;
            this.template_id = templateId;
            this.url = url;
            this.miniprogram = miniprogram;
            this.data = data;
        }
    }

    #region Miniprogram

    /// <summary>
    /// Miniprogram 接口，用户可选择使用 page 或者 pagepath 参数，
    /// 可使用 <see cref="Miniprogram_Page"/> 或 <see cref="Miniprogram_PagePath"/>
    /// </summary>
    public interface IMiniprogram
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        string appid { get; set; }
    }

    /// <summary>
    /// 小程序信息（使用 page 参数）
    /// <para><see href="https://weixin.senparc.com/QA-17333"/></para>
    /// </summary>
    public class Miniprogram_Page: IMiniprogram
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 页面路径，如：index?foo=bar
        /// <para><see href="https://weixin.senparc.com/QA-17333"/></para>
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// 小程序AppId
        /// </summary>
        /// <param name="appid">小程序AppId</param>
        /// <param name="page">页面路径，如：index?foo=bar，<see href="https://weixin.senparc.com/QA-17333"/></param>
        public Miniprogram_Page(string appid, string page)
        {
            this.appid = appid;
            this.page = page;
        }
    }

    /// <summary>
    /// 小程序信息（使用 pagepath 参数）
    /// <para><see href="https://github.com/JeffreySu/WeiXinMPSDK/issues/2554"/></para>
    /// </summary>
    public class Miniprogram_PagePath: IMiniprogram
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 页面路径，如：index?foo=bar
        /// <para><see href="https://github.com/JeffreySu/WeiXinMPSDK/issues/2554"/></para>
        /// </summary>
        public string pagepath { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid">小程序AppId</param>
        /// <param name="pagepath">页面路径，如：index?foo=bar，<see href="https://github.com/JeffreySu/WeiXinMPSDK/issues/2554"/></param>
        public Miniprogram_PagePath(string appid, string pagepath)
        {
            this.appid = appid;
            this.pagepath = pagepath;
        }
    }

    #endregion
}
