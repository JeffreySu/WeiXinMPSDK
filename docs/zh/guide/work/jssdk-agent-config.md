# JSSDK(agentConfig)

`wx.agentConfig()` 用于某些特定的接口（如审批流接口、剪切板接口等），但一般前提都需要先使用到常规的 JSSDK（参考【JSSDK（常规）】标签），因此这是一个增加项。

`wx.agentConfig()` 同样分为服务器端和客户端两部分。

## 服务端获取签名信息

后端除了后端通过 `JSSDKHelper.GetJsSdkUiPackageAsync()` 方法获取常规 JSSDK 运行所需参数以外，还需要使用同一个方法，传入不同参数来获取 `agetntConfig` 的对应参数：

```JS
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
```

> 注意：上述方法中，使用了
>
> `Senparc.Weixin.Config.SenparcWeixinSetting["企业微信审批"]`
>
> 来获取与之前不同的微信配置，请参考本项目中
>
> `appsetting.json`
>
> 文件中的设置方法：
>
> ```JSON
> "Items": {
> //添加多个企业微信应用
> "企业微信审批": {
>  "WeixinCorpId": "#{WeixinCorpId2}#",
>  "WeixinCorpAgentId": "#{WeixinCorpAgentId2}#",
>  "WeixinCorpSecret": "#{WeixinCorpSecret2}#",
>  "WeixinCorpToken": "#{WeixinCorpToken2}#",
>  "WeixinCorpEncodingAESKey": "#{WeixinCorpEncodingAESKey2}#"}
> }
> ```
>
> 此方法对所有其他模块也通用（如公众号、小程序、微信支付等）。

> 本项目参考文件：
>
> /Controllers/**_JSSDKnController.cs_** - AgentConfig() 方法
>
> /**_appsettings.json_**

## 网页端配置 JSSDK

网页端除了进行常规的 JSSDK 配置以外，还需要在执行特定的 JsApi 方法之前，添加 `wx.agentConfig()` 的配置：

```cs
function invoke(){
    wx.agentConfig({
        corpid: '@ViewData["corpId"]', // 必填，企业微信的corpid，必须与当前登录的企业一致
        agentid: '@ViewData["agentId"]', // 必填，企业微信的应用id （e.g. 1000247）
        timestamp: @agentJsApiUiPackage.Timestamp, // 必填，生成签名的时间戳
        nonceStr: '@agentJsApiUiPackage.NonceStr', // 必填，生成签名的随机串
        signature: '@agentJsApiUiPackage.Signature',// 必填，签名，见附录-JS-SDK使用权限签名算法
        jsApiList: ['thirdPartyOpenPage'], //必填，传入需要使用的接口名称
        success: function(res) {

            // 回调
            wx.invoke('thirdPartyOpenPage', {
                "oaType": "10001",// String
                //"templateId": "C4NxepvGj51gbkeGXHQgYRArW96WrxRinNfyCxo7N",//SYS
                "templateId":"247bcb886d0374a0a1f749c52794ba1a_622421053",// Open
                "thirdNo": "",// String
                "extData": {
                    'fieldList': [{
                        'title': '审批类型',
                        'type': 'text',
                        'value': '文章审批',
                    },
                    {
                        'title': '预览',
                        'type': 'link',
                        'value': 'https://weixin.senparc.com',
                    }],
                }
            },
            function(res) {
                // 输出接口的回调信息
                console.log(res);
                alert('wx.invoke result:'+JSON.stringify(res));
            });
        },
        fail: function(res) {
            if(res.errMsg.indexOf('function not exist') > -1){
                alert('版本过低请升级')
            }
            else{
                alert('wx.invoke fail:'+JSON.stringify(res));
            }
        }
    });
}
```

HTML 页面进行触发：`<a href="javascript:void(0)" onclick="invoke()">点击唤起审批流程</a>`

> 本项目参考文件：
>
> /Views/**_JSSDK/Index.cshtml_**
