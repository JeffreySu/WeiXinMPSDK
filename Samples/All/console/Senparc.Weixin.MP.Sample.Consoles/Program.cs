#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/Senparc/Senparc.CO2NET/blob/master/LICENSE

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：Program.cs
    文件功能描述：Console 示例（同样适用于 WinForm 和 WPF）


    创建标识：Senparc - 20190108

    修改标识：Senparc - 20221214
    修改描述：升级至 .NET 8.0

----------------------------------------------------------------*/

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.RegisterServices;

var dt_Start = SystemTime.Now;

var builder = new ConfigurationBuilder();
var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "";

builder.AddJsonFile("appsettings.json", false, true);
if (environment.IsNullOrEmpty() is false)
{
    builder.AddJsonFile($"appsettings.{environment}.json", false, true);
}
Console.WriteLine("完成 appsettings.json 添加");

var config = builder.Build();
Console.WriteLine("完成 ServiceCollection 和 ConfigurationBuilder 初始化");

//更多绑定操作参见：https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
var senparcSetting = config.GetSection("SenparcSetting").Get<SenparcSetting>();
var senparcWeixinSetting = config.GetSection("SenparcWeixinSetting").Get<SenparcWeixinSetting>();

var services = new ServiceCollection();
services.AddMemoryCache();//使用本地缓存必须添加

var dt_AddSdkStart = SystemTime.Now;

#region 添加微信配置（一行代码）

//Senparc.Weixin 注册（必须）
services.AddSenparcWeixinServices(config);

#endregion

var dt_AddSdkEnd = SystemTime.Now;


var app = builder.Build();

var dt_UseSdkStart = SystemTime.Now;

#region 启用微信配置（一句代码）

//手动获取配置信息可使用以下方法
//var senparcWeixinSetting = app.Services.GetService<IOptions<SenparcWeixinSetting>>()!.Value;

//启用微信配置（必须）
var registerService = app.UseSenparcWeixin(
    senparcSetting /* 不为 null 则覆盖 appsettings  中的 SenpacSetting 配置*/,
    senparcWeixinSetting /* 不为 null 则覆盖 appsettings  中的 SenpacWeixinSetting 配置*/,
    register => { /* CO2NET 全局配置 */ },
    (register, weixinSetting) =>
    {
        //注册公众号信息（可以执行多次，注册多个公众号）
        register.RegisterMpAccount(weixinSetting, "【盛派网络小助手】公众号");
    });

#endregion

var dt_UseSdkEnd = SystemTime.Now;


/* 开始使用
 * 此处可以添加高级接口（如推送消息）等业务逻辑，或添加监听
 */

/*
var weixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting;

Console.WriteLine("## weixinSetting");
Console.WriteLine(weixinSetting.ToJson(true, new Newtonsoft.Json.JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore }));
Console.WriteLine();
Console.WriteLine("## 【盛派网络小助手】公众号");
Console.WriteLine(weixinSetting.Items["【盛派网络小助手】公众号"].ToJson(true));
*/

Console.WriteLine();
var totalMs = (dt_AddSdkEnd - dt_AddSdkStart).TotalMilliseconds + (dt_UseSdkEnd - dt_UseSdkStart).TotalSeconds.ToString("#.##");
Console.WriteLine($"Senparc.Weixin SDK 已启动。");
Console.WriteLine($"程序完整启动时间：{SystemTime.DiffTotalMS(dt_Start).ToString("#.##")}毫秒，其中 Senparc.Weixin SDK 启动总用时：{totalMs}毫秒");
Console.WriteLine();
Console.WriteLine("开源示例地址：https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Samples/All/console");
Console.WriteLine();
Console.WriteLine("输入 exit 退出程序...");

while (Console.ReadLine() != "exit")
{
    continue;
}