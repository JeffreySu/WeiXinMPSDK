# JSSDK（常规）

JSSDK 用于提供微信内置浏览器接口的能力，例如转发控制、调用摄像头权限（拍照、视频）、文件上传、关闭窗口、唤起扫码窗口，等等。

要在内置浏览器中只用 JSSDK，分为“服务端获取签名信息”和“网页端配置 JSSDK”两步。

企业微信的网页端 JSSDK 配置，和公众号类似，不过多了一个名为 `wx.agentConfig()` 的配置方法。这里介绍常规的方法，`wx.agentConfig()` 配套方法请见【JSSDK（agentConfig】标签。

## 服务端获取签名信息

后端通过 `JSSDKHelper.GetJsSdkUiPackageAsync()` 方法即可自动获取前端所需的所有 JSSDK 运行所需参数：

```cs
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
```

> 本项目参考文件：
>
> /Controllers/**_JSSDKnController.cs_** - Index() 方法

## 网页端配置 JSSDK

后端配置完成的参数，直接在前端 JS 中使用 `wx.config` 进行设置，例如以下代码将完成从企业微信内转发网页到**个人微信**：

```cs
$(function(){
    wx.config({
            beta: true,// 必须这么写，否则wx.invoke调用形式的jsapi会有问题
            debug: true, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: '@jsApiUiPackage.AppId', // 必填，企业微信的corpID
            timestamp: @jsApiUiPackage.Timestamp, // 必填，生成签名的时间戳
            nonceStr: '@jsApiUiPackage.NonceStr', // 必填，生成签名的随机串
            signature: '@jsApiUiPackage.Signature',// 必填，签名，见 附录-JS-SDK使用权限签名算法
            jsApiList: ['shareWechatMessage'] // 必填，需要使用的JS接口列表，凡是要调用的接口都需要传进来
        });

        wx.checkJsApi({
            jsApiList: ['shareWechatMessage'], // 需要检测的JS接口列表，所有JS接口列表见附录2,
            success: function(res) {
                // 以键值对的形式返回，可用的api值true，不可用为false
                // 如：{"checkResult":{"chooseImage":true},"errMsg":"checkJsApi:ok"}
                //alert("wx.config success:"+JSON.stringify(res));
            }
        });
});

wx.ready(function(){
    // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
});

wx.error(function(res){
    // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
    console.log(res);
    alert(res);
});

function invoke(){
    wx.invoke(
    "shareWechatMessage", {
            title: '企业微信JSSDK 演示-转发', // 分享标题
            desc: '来自 Senparc.Weixin.Work', // 分享描述
            link: 'https://sdk.work.weixin.senparc.com/JSSDK', // 分享链接
            imgUrl: '' // 分享封面
    }, function(res) {
    if (res.err_msg == "shareWechatMessage:ok") {
        }
        }
    );
}
```

HTML 页面进行触发：`<a href="javascript:void(0)" onclick="invoke()">转发到微信</a>`

> 本项目参考文件：
>
> /Views/JSSDK/**_Index.cshtml_**

更多设置详情请参考：[JS-SDK 说明文档](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/JS-SDK.html)。
