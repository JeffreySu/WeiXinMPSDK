# OAuth 2.0

When you need to get the user's UserId, avatar, salutation and other information on the web page, you need to use OAuth 2.0 to communicate with WeChat server.

For more information, please refer to the official document: [Web Authorisation Login](https://developer.work.weixin.qq.com/document/path/91335).

The SDK has already encapsulated all the related processes, you just need to refer to the example for a simple 3-step configuration.

## Step 1: Set up the login page

In the login page, you need to set the official OAuth 2.0 request URL (called **AuthorizeUrl**) and bring the returnUrl after successful login.

Since WeChat has two types of authorisation: **snsapi_userinfo** and **snsapi_base**, and enterprise self-built apps use **snsapi_base**, this is the method used in this example to introduce the way of obtaining **AuthorizeUrl** from **snsapi_base** (the default). This method is also compatible in all scenarios:

```cs
public IActionResult Index(string returnUrl)
{
    // Set your own URL
    var url = "https://4424-222-93-135-159.ngrok.io";

    // This page directs the user to click on authorisation
    var oauthUrl =
        OAuth2Api.GetCode(_corpId, $"{url}/OAuth2/BaseCallback?returnUrl={returnUrl.UrlEncode()}",
            null, null);//snsapi_base way callback address

    ViewData["UrlBase"] = oauthUrl;
    ViewData["returnUrl"] = returnUrl;

    return View();
}
```

The above `returnUrl` parameter is usually the URL before jumping to the login page, or it can be the URL you want the user to jump to after completing the authorisation.

> Note: The above URL and path need to be matched to the address of your own server in the public backend (refer to the document: [Web Authorisation Login](https://developer.work.weixin.qq.com/document/path/91335)).

> Reference document for this project:
>
> /Controllers/OAuth2Controller.cs

## Step 2: Front-end login page setup

The ultimate function of the login page is to direct the user to open the **AuthorizeUrl**, which can be done using a direct connection:

```cs
<a href="@ViewData["UrlBase"]">Click here to test snsapi_base</a>
```

> This item is a reference document:
>
> /Views/**_OAuth2/Index.cshtml_**

## Step 3: Configure the post-login callback page

After successful authorisation, the web page will automatically jump to the callback URL (`$"{url}/OAuth2/BaseCallback?returnUrl={returnUrl.UrlEncode()}"`) set in the first step:

```cs
public async Task BaseCallback(string code, string returnUrl)
{
​   if (string.IsNullOrEmpty(code))
   {
​	    return Content("You have declined authorisation!") ;
  }

​try
  {
​    var appKey = AccessTokenContainer.BuildingKey(_workWeixinSetting);
​    var accessToken = await AccessTokenContainer.GetTokenAsync(_corpId, _corpSecret);
​    // Get user information Test link: https://open.work.weixin.qq.com/wwopen/devtool/interface?doc_id=10019

​    var oauthResult = await OAuth2Api.GetUserIdAsync(accessToken, code);
​    var userId = oauthResult.UserId;
​    GetMemberResult result = await MailListApi.GetMemberAsync(appKey, userId);

​    If (result.errcode ! = ReturnCode_Work.Request successful)
​    {
​      return Content("Error: " + result.errmsg);
​    }

​    ViewData["returnUrl"] = returnUrl;

​    /* Caution:

​      \* In the actual scenario, you should jump to returnUrl instead of staying on the Callback page.

​      \* Because when the user refreshes the URL of this page, the actual code and other parameters are invalid, and the user will get an error message.

​      */
​      return View(result);
​    }
​    catch (Exception ex)
    {
​       return Content("Error: " + ex.Message);
    }
}
```

In the above code, the `returnUrl` is the `returnUrl` passed to **AuthorizeUrl** in the `Index()` method in the first step, when all the operations of obtaining and saving the user's information are completed, the `returnUrl` is used to jump to the page before the login, completing the whole closed-loop login operation.

> Reference document for this project:
> /Controllers/\*\*\*\*OAuth2Controller.cs
