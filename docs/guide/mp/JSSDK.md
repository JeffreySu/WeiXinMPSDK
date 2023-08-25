# JSSDK

JSSDK is used to provide WeChat's built-in browser interface capabilities, such as forwarding control, calling camera permissions (photo, video), file uploading, closing windows, evoking code scanning windows, and so on.

To use JSSDK only in the built-in browser, there are two steps: "Getting signature information on the server side" and "Configuring JSSDK on the web side".

## Server-side signature retrieval

The backend can automatically get all the JSSDK parameters needed by the frontend by using the `JSSDKHelper.GetJsSdkUiPackageAsync()` method:

```cs
public async Task Index()
{
    var jssdkUiPackage = await JSSDKHelper.GetJsSdkUiPackageAsync(appId, appSecret, Request)
    return View(jssdkUiPackage);
}
```

> Reference file for this project:
> /Controllers/**_WeixiJSSDKnController.cs_**

## Configure JSSDK on web side

Parameters configured in the back-end can be set directly in the front-end JS using `wx.config`, for example, the following code will customise the title and image of the forwarded message when forwarding a web page:

```JS
wx.config({
        debug: false, // 开启调试模式
        appId: '@Model.AppId', // 必填，公众号的唯一标识
        timestamp: '@Model.Timestamp', // 必填，生成签名的时间戳
        nonceStr: '@Model.NonceStr', // 必填，生成签名的随机串
        signature: '@Model.Signature',// 必填，签名
        jsApiList: [
                'checkJsApi',
                'onMenuShareTimeline',
                'onMenuShareAppMessage'
        ]
    });

    wx.error(function (res) {
        console.log(res);
        alert('验证失败');
    });

    wx.ready(function () {
        var url = 'https://sdk.weixin.senparc.com';
        var link = url + '/';
        var imgUrl = url + '/images/v2/ewm_01.png';

        //转发到朋友圈
        wx.onMenuShareTimeline({
            title: 'JSSDK朋友圈转发测试',
            link: link,
            imgUrl: imgUrl,
            success: function () {
                alert('转发成功！');
            },
            cancel: function () {
                alert('转发失败！');
            }
        });
        //转发给朋友
        wx.onMenuShareAppMessage({
            title: 'JSSDK朋友圈转发测试',
            desc: '转发给朋友',
            link: link,
            imgUrl: imgUrl,
            type: 'link',
            dataUrl: '',
            success: function () {
                alert('转发成功！');
            },
            cancel: function () {
                alert('转发失败！');
            }
        });
    });
```

> Tip: Passing a ViewModel in MVC needs to be defined at the top of the .cshtml file:
>
> ```cs
> @model Senparc.Weixin.MP.Helpers.JsSdkUiPackage
> ```

> Reference file for this project:
>
> /Views/WeixinJSSDK/**_Index.cshtml_**

For more setting details, please refer to: [JS-SDK description document](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/JS-SDK.html).
