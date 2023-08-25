# OAuth 2.0

When you need to get the user's OpenId, avatar, nickname and other information on the web page, you need to use OAuth 2.0 to communicate with WeChat server.

For more information, please refer to the official documentation: [Web Authorisation](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorization.html#4).

The SDK has already encapsulated all the related processes, you just need to refer to the example for a simple 3-step configuration.

## Step 1: Set up the login page

In the login page, you need to set the official OAuth 2.0 request URL (called **AuthorizeUrl**) and bring the returnUrl after successful login.

Since WeChat authorisation has two methods (**snsapi_userinfo** and **snsapi_base**), the code below directly provides two ways to get the **AuthorizeUrl**:

```cs
public ActionResult Index(string returnUrl)
{
    ViewData["returnUrl"] = returnUrl;

    // This page directs the user to click on authorisation
    ViewData["UrlUserInfo"] =
        OAuthApi.GetAuthorizeUrl(appId,
        "http://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode(),
        null, OAuthScope.snsapi_userinfo);//snsapi_userinfo method callback address

    ViewData["UrlBase"] =
        OAuthApi.GetAuthorizeUrl(appId,
        "http://sdk.weixin.senparc.com/oauth2/BaseCallback?returnUrl=" + returnUrl.UrlEncode(),
        null, OAuthScope.snsapi_base);//snsapi_base way callback address
    return View();
}
```

The above `returnUrl` parameter is usually the URL before jumping to the login page, or it can be the URL you want the user to jump to after completing the authorisation.

> Note: The above URL and path need to be matched to your own server address in the public backend (refer to the document: [Web Authorisation](https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/Wechat_webpage_authorisation.html#4)).

> Reference document for this project:
>
> /Controllers/**_OAuth2Controller.cs_**

## Step 2: Front-end login page setup

The ultimate function of the login page is to direct the user to open the **AuthorizeUrl**, which can be done using a direct connection:

```cs
<! -- snsapi_userinfo Mode Callback Address-->
<a href="@ViewData["UrlUserInfo"]">Click here to test snsapi_userinfo</a>

<! -- snsapi_base Mode Callback Address -->
<a href="@ViewData["UrlBase"]">Click here to test snsapi_userinfo</a>
```

> Documentation references for this item:
>
> /Views/OAuth2/**_Index.cshtml_**

## Step 3: Configure the post-login callback page

After successful authorisation, the webpage will automatically jump to the callback URL set in the first step (`"http://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + returnUrl.UrlEncode()`), take **UserInfoCallback** as an example:

```cs
public ActionResult UserInfoCallback(string code, string returnUrl)
{
    if (string.IsNullOrEmpty(code))
    {
        return Content("You have denied authorisation!") ;
    }

    OAuthAccessTokenResult result = null;

    // Pass, exchange code for access_token.
    {
        result = OAuthApi.GetAccessToken(appId, appSecret, code);
    }
    catch (Exception ex)
    {
        return Content(ex.Message);
    }
    if (result.errcode ! = ReturnCode.Request was successful)
    {
        return Content("Error: " + result.errmsg);
    }

    // The following 2 data can also be encapsulated into a class of their own and stored in a database (combined with caching is recommended)
    // If you can ensure the security, you can store the access_token in the user's cookie, each person's access_token is different
    HttpContext.Session.SetString("OAuthAccessTokenStartTime", SystemTime.Now.ToString());
    HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

    //Because the first step is to choose the OAuthScope.snsapi_userinfo, here you can further get user details
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

In the above code, the `returnUrl` is the `returnUrl` passed to **AuthorizeUrl** in the `Index()` method of the first step, when all the operations of obtaining and saving the user information are completed, the `returnUrl` is used to jump to the page before the login, and complete the whole closed-loop login operation.

> Reference document for this project:
>
> /Controllers/**_OAuth2Controller.cs_**
