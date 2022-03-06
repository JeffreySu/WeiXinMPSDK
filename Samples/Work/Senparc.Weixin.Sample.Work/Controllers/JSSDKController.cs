using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Helpers;

namespace Senparc.Weixin.Sample.Work.Controllers
{
    public class JSSDKController : Controller
    {
        /// <summary>
        /// 常规需求
        /// </summary>
        /// <returns></returns>
public async Task<ActionResult> Index()
{
    // 当前 URL
    var url = "https://sdk.work.weixin.senparc.com/JSSDK/";
    // 获取企业微信配置
    var workSetting = Senparc.Weixin.Config.SenparcWeixinSetting.WorkSetting;
    // 获取 JsApiTicket（保密信息，不可外传）
    var jsApiTicket = await JsApiTicketContainer.GetTicketAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, false);
    // 获取 UI 打包信息
    var jsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, jsApiTicket, false);
            
    ViewData["jsApiUiPackage"] = jsApiUiPackage;
    return View();
}

        /// <summary>
        /// 结合使用 agentConfig
        /// </summary>
        /// <returns></returns>
public async Task<ActionResult> AgentConfig()
{
    //此处演示同时支持多个应用的注册，请参考 appsettings.json 文件
    var workSetting = Senparc.Weixin.Config.SenparcWeixinSetting["企业微信审批"] as ISenparcWeixinSettingForWork;
    var url = "https://sdk.weixin.senparc.com/Work/Approval";

    //获取 UI 信息包
           
    /* 注意：
        * 所有应用中，jsApiUiPackage 是必备的
        */
    var jsApiTicket = await JsApiTicketContainer.GetTicketAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, false);
    var jsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, jsApiTicket, false);
    ViewData["jsApiUiPackage"] = jsApiUiPackage;

    /* 注意：
        * 1、这里需要使用 WeixinCorpAgentId，而不是 WeixinCorpId
        * 2、agentJsApiUiPackage 是否需要提供，请参考官方文档，此处演示了最复杂的情况
        */
    ViewData["thirdNo"] = DateTime.Now.Ticks + Guid.NewGuid().ToString("n");
    ViewData["corpId"] = workSetting.WeixinCorpId;
    ViewData["agentId"] = workSetting.WeixinCorpAgentId;
    var agentConfigJsApiTicket = await JsApiTicketContainer.GetTicketAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, true);
    var agentJsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, agentConfigJsApiTicket, true);
    ViewData["agentJsApiUiPackage"] = agentJsApiUiPackage;

    return View();
}
    }
}
