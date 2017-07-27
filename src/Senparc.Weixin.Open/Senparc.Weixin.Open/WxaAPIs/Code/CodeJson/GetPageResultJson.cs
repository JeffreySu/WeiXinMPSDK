/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：GetPageResultJson.cs
    文件功能描述：首页和每页信息返回结果
    
    
    创建标识：Senparc - 20170726


----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetPageResultJson : WxJsonResult
    {
        public List<PageInfo> page_list { get; set; }
    }


    [Serializable]
    public class PageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string index { get; set; }

        public string page_list { get; set; }

        public string page_detail { get; set; }
    }
}
