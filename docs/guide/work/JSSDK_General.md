# JSSDK (General)

JSSDK is used to provide WeChat's built-in browser interface capabilities, such as forwarding control, calling camera permissions (photo, video), file uploading, closing windows, evoking code scanning windows, and so on.

To use the JSSDK only in the built-in browser, there are two steps: "Get signature information from server side" and "Configure JSSDK on web side".

The web-side JSSDK configuration of enterprise WeChat is similar to that of the public website, but there is an additional configuration method named `wx.agentConfig()`. Here we introduce the regular method, `wx.agentConfig()` supporting method please see [JSSDK (agentConfig)] tag.

## Getting signature information on the server side

The backend can automatically get all the JSSDK parameters needed for the frontend to run by using the `JSSDKHelper.GetJsSdkUiPackageAsync()` method:

```cs
public async Task<ActionResult> Index()
{
    // Current URL
    var url = "https://sdk.work.weixin.senparc.com/JSSDK/";
    // Get the enterprise weixin configuration
    var workSetting = Senparc.Weixin.Config.
    // Get the JsApiTicket (confidential information, not to be shared)
    var jsApiTicket = await JsApiTicketContainer.GetTicketAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, false); // Get the UI packaging information.
    // Get the UI package information
    var jsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting.WeixinCorpId, workSetting.WeixinCorpSecret, url, jsApiTicket, false); // Get the UI packaged information. var jsApiUiPackage = await JSSDKHelper.GetJsApiUiPackageAsync(workSetting. false);

ViewData["jsApiUiPackage"] = jsApiUiPackage;
return View();

}
```

> Reference documentation for this project:
>
> /Controllers/**_JSSDKnController.cs_** - Index() method

## Configure JSSDK on the web side

Parameters configured in the back-end are set directly in the front-end JS using `wx.config`, e.g. the following code will complete the forwarding of a web page from within an enterprise WeChat to a **personal WeChat**:

```cs
$(function(){
    wx.config({
            beta: true, // must be written this way, otherwise there will be problems with the wx.invoke call form jsapi
            debug: true, // enable debug mode, the return value of all api calls will be alerted on the client side, if you want to see the incoming parameters, you can open it on the pc side, and the parameter information will be typed out through the log, and it will only be printed when it is on the pc side.
            appId: '@jsApiUiPackage.AppId', // mandatory, the corpID of enterprise weibo
            timestamp: @jsApiUiPackage.Timestamp, // mandatory, the timestamp for signature generation
            nonceStr: '@jsApiUiPackage.
            signature: '@jsApiUiPackage.Signature', // mandatory, signature, see Appendix - JS-SDK Signature Algorithm for Using Permissions
            jsApiList: ['shareWechatMessage'] // mandatory, a list of JS interfaces to be used, any interface to be called needs to be passed in
        });

​        wx.checkJsApi({
​            jsApiList: ['shareWechatMessage'], // list of JS interfaces to be checked, see Appendix 2 for a list of all JS interfaces, }
​            success: function(res) {
​                // Return as a key-value pair, true for available api values, false for unavailable ones.
​                // e.g. {"checkResult":{"chooseImage":true}, "errMsg": "checkJsApi:ok"}
​                //alert("wx.config success: "+JSON.stringify(res));;
​            }
​        }).
});

wx.ready(function(){
    //  Config information will be validated after the execution of the ready method, all interfaces must be called after the result of the config interface, config is a client-side asynchronous operation, so if you need to call the relevant interfaces when the page is loaded, you must put the relevant interfaces in the ready function to ensure the correct implementation.For interfaces that are called only when triggered by the user, they can be called directly and do not need to be placed in the ready function.
});

wx.error(function(res){
    // Config information validation failure will execute the error function, such as signature expiration caused by the validation failure, the specific error message can open the debug mode of the config to view, you can also view the res parameter in the return, for SPA can be updated here signature.
    console.log(res);
    alert(res);
});

function invoke(){
    wx.invoke(
    "shareWechatMessage", {
            title: 'Enterprise WeChat JSSDK Demo - Forward', // Share title
            desc: 'From Senparc.Weixin.Work', // share description
            link: 'https://sdk.work.weixin.senparc.com/JSSDK', // Share link
            imgUrl: '' // Share the cover
    }, function(res) {
    if (res.err_msg == "shareWechatMessage:ok") {
        }
        }
    );
}
```

HTML page to trigger: `<a href="javascript:void(0)" onclick="invoke()">Forward to WeChat</a>`

> Reference document for this project:
>
> /Views/JSSDK/**_Index.cshtml_**

For more setting details, please refer to: [JS-SDK description document](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/JS-SDK.html).
