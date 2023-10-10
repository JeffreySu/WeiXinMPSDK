# OAuth 2.0

当你需要在网页上获取用户的 OpenId、头像、昵称等信息的时候，就需要使用 OAuth 2.0 的方式和微信服务器通讯。

更多信息请参考官方文档：[网页授权](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html#4)。

SDK 已经封装了所有相关的过程，您只需要参考示例进行简单的 3 步配置即可。

## 第一步：设置登录页面

登录页面中需要设置官方 OAuth 2.0 的请求 URL（称为 **AuthorizeUrl**），并带上登录成功后的 returnUrl。

由于微信授权具有两种方式（**snsapi_userinfo** 和 **snsapi_base**，下方代码直接提供了两种获取 **AuthorizeUrl** 的方式：

```cs
public ActionResult Index(string returnUrl)
{
    ViewData["returnUrl"] = returnUrl;

    //此页面引导用户点击授权
    ViewData["UrlUserInfo"] =
        OAuthApi.GetAuthorizeUrl(appId,
        "http://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode(),
        null, OAuthScope.snsapi_userinfo);//snsapi_userinfo方式回调地址

    ViewData["UrlBase"] =
        OAuthApi.GetAuthorizeUrl(appId,
        "http://sdk.weixin.senparc.com/oauth2/BaseCallback?returnUrl=" + returnUrl.UrlEncode(),
        null, OAuthScope.snsapi_base);//snsapi_base方式回调地址
    return View();
}
```

上述 `returnUrl` 参数一般为跳转到登陆页面之前的 URL，也可以是希望用户完成授权之后跳转到的 URL。

> 注意：上述的网址和路径需要在公众号后台匹配成你自己服务器的地址（参考文档：[网页授权](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html#4)）。

> 本项目参考文件：
>
> /Controllers/**_OAuth2Controller.cs_**

## 第二步：前端登录页面设置

登录页面最终的功能是引导用户打开 **AuthorizeUrl**，可以直接使用连接的方式：

```cs
<!-- snsapi_userinfo方式回调地址 -->
<a href="@ViewData["UrlUserInfo"]">点击这里测试snsapi_userinfo</a>

<!-- snsapi_base方式回调地址 -->
<a href="@ViewData["UrlBase"]">点击这里测试snsapi_userinfo</a>
```

> 本项目参考文件：
>
> /Views/OAuth2/**_Index.cshtml_**

## 第三步：配置登陆后回调页面

授权成功后，网页将自动跳转到第一步中设置的回调 URL（`"http://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode()`），以 **UserInfoCallback** 为例：

```cs
public ActionResult UserInfoCallback(string code, string returnUrl)
{
    if (string.IsNullOrEmpty(code))
    {
        return Content("您拒绝了授权！");
    }

    OAuthAccessTokenResult result = null;

    //通过，用code换取access_token
    try
    {
        result = OAuthApi.GetAccessToken(appId, appSecret, code);
    }
    catch (Exception ex)
    {
        return Content(ex.Message);
    }
    if (result.errcode != ReturnCode.请求成功)
    {
        return Content("错误：" + result.errmsg);
    }

    //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
    //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
    HttpContext.Session.SetString("OAuthAccessTokenStartTime", SystemTime.Now.ToString());
    HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

    //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
    try
    {
        if (!string.IsNullOrEmpty(returnUrl))
        {
            return Redirect(returnUrl);
        }

        OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
        return View(userInfo);
    }
    catch (ErrorJsonResultException ex)
    {
        return Content(ex.Message);
    }
}
```

上述代码中，传入的 `returnUrl` 即第一步 `Index()` 方法中传入到 **AuthorizeUrl** 中的 `returnUrl`，当所有用户信息获取、保存等操作完成后，借助 `returnUrl` 跳转到登陆之前的页面，完成整个闭环的登录操作。

> 本项目参考文件：
>
> /Controllers/**_OAuth2Controller.cs_**
