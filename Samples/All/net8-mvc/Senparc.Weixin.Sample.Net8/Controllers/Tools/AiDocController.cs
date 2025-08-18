/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    AiDocController.cs
    文件功能描述：AI 文档
    
    
    创建标识：Senparc - 20250818

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Agents;
using Senparc.Weixin.MP;
using Senparc.Weixin.Tencent;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET.Cache;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Senparc.Weixin.Sample.Net8.Controllers
{
    /// <summary>
    /// AI 文档
    /// </summary>
    public class AiDocController : BaseController
    {
        IServiceProvider _serviceProvider;

        public AiDocController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return NotFound();
        }


    }
}
