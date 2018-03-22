/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：BaseController.cs
    文件功能描述：Controller基类
    
    
    创建标识：Senparc - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

#if NET45
using System.Web;
using System.Web.Mvc;
#else
using Microsoft.AspNetCore.Mvc;
#endif

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    public class BaseController : Controller
    {

    }
}
