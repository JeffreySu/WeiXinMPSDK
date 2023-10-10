# JSSDK

JSSDK 用于提供微信内置浏览器接口的能力，例如转发控制、调用摄像头权限（拍照、视频）、文件上传、关闭窗口、唤起扫码窗口，等等。

要在内置浏览器中只用 JSSDK，分为“服务端获取签名信息”和“网页端配置 JSSDK”两步。

## 服务端获取签名信息

后端通过 `JSSDKHelper.GetJsSdkUiPackageAsync()` 方法即可自动获取前端所需的所有 JSSDK 运行所需参数：

```cs
public async Task Index()
{
    var jssdkUiPackage = await JSSDKHelper.GetJsSdkUiPackageAsync(appId, appSecret, Request.AbsoluteUri());
    return View(jssdkUiPackage);
}
```

> 本项目参考文件：
>
> /Controllers/**_WeixiJSSDKnController.cs_**

## 网页端配置 JSSDK

后端配置完成的参数，直接在前端 JS 中使用 `wx.config` 进行设置，例如以下代码将完成在转发网页时自定义转发消息的标题和图片：

```js
wx.config({
  debug: false, // 开启调试模式
  appId: "@Model.AppId", // 必填，公众号的唯一标识
  timestamp: "@Model.Timestamp", // 必填，生成签名的时间戳
  nonceStr: "@Model.NonceStr", // 必填，生成签名的随机串
  signature: "@Model.Signature", // 必填，签名
  jsApiList: ["checkJsApi", "onMenuShareTimeline", "onMenuShareAppMessage"],
});

wx.error(function (res) {
  console.log(res);
  alert("验证失败");
});

wx.ready(function () {
  var url = "https://sdk.weixin.senparc.com";
  var link = url + "";
  var imgUrl = url + "/images/v2/ewm_01.png";

  //转发到朋友圈
  wx.onMenuShareTimeline({
    title: "JSSDK朋友圈转发测试",
    link: link,
    imgUrl: imgUrl,
    success: function () {
      alert("转发成功！");
    },
    cancel: function () {
      alert("转发失败！");
    },
  });
  //转发给朋友
  wx.onMenuShareAppMessage({
    title: "JSSDK朋友圈转发测试",
    desc: "转发给朋友",
    link: link,
    imgUrl: imgUrl,
    type: "link",
    dataUrl: "",
    success: function () {
      alert("转发成功！");
    },
    cancel: function () {
      alert("转发失败！");
    },
  });
});
```

> 提示：在 MVC 中传递 ViewModel 需要在 .cshtml 文件顶部定义：
>
> ```cs
> @model Senparc.Weixin.MP.Helpers.JsSdkUiPackage
> ```

> 本项目参考文件：
>
> /Views/WeixinJSSDK/**_Index.cshtml_**

更多设置详情请参考：[JS-SDK 说明文档](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/JS-SDK.html)。
