# OAuth 2.0

当你需要在网页上获取用户的 UserId、头像、称呼等信息的时候，就需要使用 OAuth 2.0 的方式和微信服务器通讯。

更多信息请参考官方文档：[网页授权登录](https://developer.work.weixin.qq.com/document/path/91335)。

SDK 已经封装了所有相关的过程，您只需要参考示例进行简单的 3 步配置即可。

## 第一步：设置登录页面

登录页面中需要设置官方 OAuth 2.0 的请求 URL（称为 **AuthorizeUrl**），并带上登录成功后的 returnUrl。

由于微信授权具有两种方式：**snsapi_userinfo** 和 **snsapi_base**，企业自建应用使用 **snsapi_base**，因此本示例中使用此方式进行介绍 **snsapi_base**（默认）的 **AuthorizeUrl** 获取的方式（此方式也是所有场景下兼容的方式）：

```cs
public IActionResult Index(string returnUrl)
{
    // 设置自己的 URL
    var url = "https://4424-222-93-135-159.ngrok.io";

    //此页面引导用户点击授权
    var oauthUrl =
        OAuth2Api.GetCode(_corpId, $"{url}/OAuth2/BaseCallback?returnUrl={returnUrl.UrlEncode()}",
            null, null);//snsapi_base方式回调地址

    ViewData["UrlBase"] = oauthUrl;
    ViewData["returnUrl"] = returnUrl;

    return View();
}
```

上述 `returnUrl` 参数一般为跳转到登陆页面之前的 URL，也可以是希望用户完成授权之后跳转到的 URL。

> 注意：上述的网址和路径需要在公众号后台匹配成你自己服务器的地址（参考文档：[网页授权登录](https://developer.work.weixin.qq.com/document/path/91335)）。

> 本项目参考文件：
>
> /Controllers/OAuth2Controller.cs

## 第二步：前端登录页面设置

登录页面最终的功能是引导用户打开 **AuthorizeUrl**，可以直接使用连接的方式：

```
<a href="@ViewData["UrlBase"]">点击这里测试snsapi_base</a>
```

> 本项目参考文件：
>
> /Views/**_OAuth2/Index.cshtml_**

## 第三步：配置登陆后回调页面

授权成功后，网页将自动跳转到第一步中设置的回调 URL（`$"{url}/OAuth2/BaseCallback?returnUrl={returnUrl.UrlEncode()}"`）：

```cs
public async Task BaseCallback(string code, string returnUrl)
{
    if (string.IsNullOrEmpty(code))
    {
        return Content("您拒绝了授权！");
    }

    try
    {
        var appKey = AccessTokenContainer.BuildingKey(_workWeixinSetting);
        var accessToken = await AccessTokenContainer.GetTokenAsync(_corpId, _corpSecret);
        //获取用户信息 测试链接：https://open.work.weixin.qq.com/wwopen/devtool/interface?doc_id=10019
        var oauthResult = await OAuth2Api.GetUserIdAsync(accessToken, code);
        var userId = oauthResult.UserId;
        GetMemberResult result = await MailListApi.GetMemberAsync(appKey, userId);

        if (result.errcode != ReturnCode_Work.请求成功)
        {
            return Content("错误：" + result.errmsg);
        }

        ViewData["returnUrl"] = returnUrl;

        /* 注意：
        * 实际适用场景，此处应该跳转到 returnUrl，不要停留在 Callback页面上。
        * 因为当用户刷新此页面 URL 时，实际 code 等参数已失效，用户会受到错误信息。
        */
        return View(result);
    }
    catch (Exception ex)
    {
        return Content("错误：" + ex.Message);
    }
}
```

上述代码中，传入的 `returnUrl` 即第一步 `Index()` 方法中传入到 **AuthorizeUrl** 中的 `returnUrl`，当所有用户信息获取、保存等操作完成后，借助 `returnUrl` 跳转到登陆之前的页面，完成整个闭环的登录操作。

> 本项目参考文件：
>
> /Controllers/**_OAuth2Controller.cs_**
