/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：SubmitAuditPageInfo.cs
    文件功能描述：小程序页面返回结果
    
    
    创建标识：Senparc - 20170726

    注意：此项目是《微信开发深度解析：微信公众号、小程序高效开发秘籍》图书中第5章的WeixinMarketing项目源代码。
    本项目只包含了运行案例所必须的学习代码，以及精简的部分SenparcCore框架代码，不确保其他方面的稳定性、安全性，
    因此，请勿直接用于商业项目，例如安全性、缓存等需要根据具体情况进行调试。

    盛派网络保留所有权利。

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class SubmitAuditPageInfo
    {
        /// <summary>
        /// 小程序的页面
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 小程序的标签，多个标签用空格分隔，标签不能多于10个，标签长度不超过20
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// 一级类目
        /// </summary>
        public string first_class { get; set; }

        /// <summary>
        /// 二级类目
        /// </summary>
        public string second_class { get; set; }

        /// <summary>
        /// 三级类目
        /// </summary>
        public string third_class { get; set; }

        /// <summary>
        /// 一级类目的ID
        /// </summary>
        public int first_id { get; set; }

        /// <summary>
        /// 二级类目的ID
        /// </summary>
        public int second_id { get; set; }

        /// <summary>
        /// 三级类目的ID
        /// </summary>
        public int third_id { get; set; }

        /// <summary>
        /// 小程序页面的标题,标题长度不超过32
        /// </summary>
        public string title { get; set; }
    }
}
