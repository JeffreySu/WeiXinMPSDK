/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WiFiRegisterJsonResult.cs
    文件功能描述：添加portal型设备的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 添加portal型设备的返回结果
    /// </summary>
    public class WiFiRegisterJsonResult : WxJsonResult 
    {
        public Register_Data data { get; set; }

       
    }
    public class Register_Data
    {
        public string secretkey { get; set; }
    }
}
