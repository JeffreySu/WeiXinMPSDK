# JSSDK(agentConfig)

`wx.agentConfig()` is used for some specific interfaces (e.g. approval stream interface, clipboard interface, etc.), but generally the prerequisites need to be used first with the regular JSSDK (refer to the [JSSDK (Regular)] tag), so this is an addition.

The `wx.agentConfig()` is also divided into two parts, server-side and client-side.

## Server-side getting signature information

The backend, in addition to the backend's `JSSDKHelper.GetJsSdkUiPackageAsync()` method to get the parameters needed to run the regular JSSDK, also needs to use the same method, passing in different parameters to get the corresponding parameters for `agetntConfig`:

```cs
public async Task<ActionResult> AgentConfig()
{
    //This demonstrates the support of multiple apps registration at the same time, please refer to appsettings.json file.
    var workSetting = Senparc.Weixin.Config.SenparcWeixinSetting["Enterprise Weixin Approval"] as ISenparcWeixinSettingForWork; var workSetting = Senparc.Weixin.Config.
    var url = "https://sdk.weixin.senparc.com/Work/Approval";

    // Get the UI information package

    /* Caution:
    * jsApiUiPackage is required for all applications
    */
    var jsApiTicket = await JsApiTicketContainer.GetTicketAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, false);
    var jsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, jsApiTicket, false);
    ViewData["jsApiUiPackage"] = jsApiUiPackage;

    /* Notes:
    * 1, here you need to use WeixinCorpAgentId, not WeixinCorpId
    * 2, agentJsApiUiPackage whether need to provide, please refer to the official document, here demonstrates the most complex case
    */
    ViewData["thirdNo"] = DateTime.Now.Ticks + Guid.NewGuid().ToString("n");
    ViewData["corpId"] = workSetting.WeixinCorpId;
    ViewData["agentId"] = workSetting.WeixinCorpAgentId;
    var agentConfigJsApiTicket = await JsApiTicketContainer.GetTicketAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, true);
    var agentJsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, agentConfigJsApiTicket, true);
    ViewData["agentJsApiUiPackage"] = agentJsApiUiPackage;

    return View();
}
```

> Note: The above method uses the
>
> `Senparc.Weixin.Config.SenparcWeixinSetting["Enterprise Weixin Approval"] `
>
> to get a different Weixin configuration from the previous one, please refer to this project's
>
> `appsetting.json`
>
> file for the setting method:
>
> ```json
> "Items": {
> // Add multiple Enterprise WeChat apps
> "Enterprise Weixin Approval": {
>  "WeixinCorpId": "#{WeixinCorpId2}#",
>  "WeixinCorpAgentId": "#{WeixinCorpAgentId2}#",
>  "WeixinCorpSecret": "#{WeixinCorpSecret2}#",
>  "WeixinCorpToken": "#{WeixinCorpToken2}#",
>  "WeixinCorpEncodingAESKey": "#{WeixinCorpEncodingAESKey2}#"}
> }
> ```
>
> This method is also common to all other modules (e.g. Public, Applet, Weixin Pay, etc.).

> Reference file for this project:
>
> /Controllers/**_JSSDKnController.cs_** - AgentConfig() method
>
> /**_appsettings.json_**

## Configuring the JSSDK on the web side

In addition to the usual JSSDK configuration, the web side needs to add `wx.agentConfig()`'' configuration before executing specific JsApi methods:

```cs
function invoke(){
    wx.agentConfig({
        corpid: '@ViewData["corpId"]', // mandatory, the corpid of the enterprise wx, must be the same as the currently logged in enterprise
        agentid: '@ViewData["agentId"]', // mandatory, the application id of enterprise weibo (e.g. 1000247)
        timestamp: @agentJsApiUiPackage.Timestamp
        nonceStr: '@agentJsApiUiPackage.NonceStr', // Mandatory, random string to generate the signature
        signature: '@agentJsApiUiPackage.Signature', // mandatory, signature, see Appendix - JS-SDK Signature Algorithm for Using Permissions
        jsApiList: ['thirdPartyOpenPage'], // Required, pass in the name of the interface to be used
        success: function(res) {

​        // Callback
​        wx.invoke('thirdPartyOpenPage', {
​            "oaType": "10001",// String
​            // "templateId": "C4NxepvGj51gbkeGXHQgYRArW96WrxRinNfyCxo7N",// SYS
​            "templateId": "247bcb886d0374a0a1f749c52794ba1a_622421053",// Open
​            "thirdNo": "",// String
​            "extData": {
​                'fieldList': [{
​                    'title': 'Approval Type', // String "extData": { 'fieldList': [{ 'title': 'Approval Type', {
​                    'type': 'text',
​                    'value': 'Article Approval',
​                },
​                {
​                    'title': 'Preview',
​                    'type': 'link',
​                    'value': 'https://weixin.senparc.com',
​                }]
​            }
​        },
​        function(res) {
​            // Output the interface's callback information
​            console.log(res);
​            alert('wx.invoke result:'+JSON.stringify   (res));
​       });
},
    fail: function(res)  { // output console.
​       if(res.errMsg.indexOf('function not exist') > -1)
        {
​            alert('Version too low, please upgrade')
​        }
​        else
        {
​           alert('wx.invoke fail:'+JSON.stringify(res));
​        }
​   	 }
	});
}
```

HTML page to trigger: `<a href="javascript:void(0)" onclick="invoke()">Click to invoke the approval process</a>`.

> Reference document for this project:
>
> /Views/JSSDK/**_Index.cshtml_**
