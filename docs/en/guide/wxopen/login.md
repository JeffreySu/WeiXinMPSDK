# Sign in

## Introduction

Most applets need to identify the user's identity (OpenId) and obtain the user's avatar, nickname and other information to provide user identification, personalised information display services. In this case, it is necessary to use the applet login interface.

## Client - enter the login page

User login is initiated on the applet side. Generally speaking, a separate page can be made for placing instructions and login buttons. Firstly, an entry button is placed on the entry page:

```cs
<button bindtap="getUserInfo"> Get Avatar Nickname </button>
```

Clicking the button triggers the `getUserInfo()` method in the .js file:

```js
 getUserInfo: function(){
    wx.navigateTo({
      url: '../Login/Login',
    })
  }
```

This button will direct the page to a separate Login page.

![跳转到登录页](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-01.png)

> Reference files for this project: Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.wxml_** Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.js_**

## Client - Login Page

The login page contains the system login display, privacy agreement, etc., and includes a button that triggers the final applet client login event:

```xml
<!--pages/Login/Login.wxml-->
<view class="auth-notice">
      <text class="auth-notice">Hello，</text><open-data type="userNickName" id="login-nickname"></open-data>，
      <text class="auth-notice">The current applet is a functional experience applet for Senparc.Weixin SDK, including subscription messages, get user authorisation information, mobile phone number, WebSocket, customer service messages and other demo content, most of the content needs to be authorised to carry out, click on the [Get Avatar Nickname] button for authorisation in order to enter the test page. </text>
      <text class="auth-notice">If you do not wish to authorise, please simply close this page. </text>
      <button class="auth-btn" bindtap="getUserInfo"> Get avatar nicknames  </button>
</view>
```

![登录页面](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-02.png)

> Reference documents for this item:
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/Login/Login.wxml_**

The [Get Avatar Nickname] button binds the method `getUserInfo()`, which is added in **Login.js**:

```js
var app = getApp()
getUserInfo: function (e) {
    var that = this;
    app.getUserInfo(e, function(userInfo){
      app.globalData.userInfo = userInfo
      that.setData({
        userInfo: userInfo, hasUserInfo: userInfo, hasUserInfo.
        hasUserInfo: true
      });
      wx.navigateTo({
        url: '... /index/index',
      })
    });
  }
```

> Reference file for this project:
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/Login/Login.js_**

In the above code, global methods are introduced using `var app = getApp()` (the code is in the root directory **app.js**), where the `app.getUserInfo()` method code is as follows (in order to be closer to the actual usage scenarios, we show **Login** + **GetUserInfo** in a coherent code):

```js
getUserInfo:function(cb,callback){
    var that = this
    if(this.globalData.userInfo){
      typeof cb == "function" && cb(this.globalData.userInfo)
    }else{
    // Get the userInfo and check it.
    console.log('Preparing to call wx.getUserProfile');
    wx.getUserProfile({
      desc: 'Used to complete member profile', // Declare the use of the user's personal information after obtaining it, which will be displayed in a popup window later, please fill in with caution
      success: function (userInfoRes) {
        console.log('getUserProfile', userInfoRes);
        that.globalData.userInfo = userInfoRes.userInfo
        typeof cb == "function" && cb(that.globalData.userInfo)
        typeof callback == "function" && callback(userInfoRes.userInfo)

        // Call the login interface
        wx.login({
            success: function (res) {
              // swap openid & session_key
              wx.request({
                url: wx.getStorageSync('domainName') + '/WxOpen/OnLogin',
                method: 'POST',
                header: { 'content-type': 'application/x-www-form-urlencoded' }
                data: {
                  code: res.code
                }, success:function(json)
                success:function(json){
                  console.log('wx.login - request-/WxOpen/OnLogin Result:', json);
                  var result = json.data;
                  if(result.success)
                  {
                    wx.setStorageSync('sessionId', result.sessionId);
                    // Checksum
                    wx.request({
                      url: wx.getStorageSync('domainName') + '/WxOpen/CheckWxOpenSignature',
                      method: 'POST',
                      header: { 'content-type': 'application/x-www-form-urlencoded' }
                      data: {
                        sessionId: result.sessionId,//wx.getStorageSync('sessionId'),
                        rawData:userInfoRes.rawData,
                        signature:userInfoRes.signature
                      },
                      success:function(json){
                        console.log(json.data);
                        if(!json.data.success){
                          alert(json.data.msg);
                        }
                      }
                    });

                    // decrypt the data (recommended to be put in the checksum success callback function, here for demonstration only)
                    wx.request({
                      url: wx.getStorageSync('domainName') + '/WxOpen/DecodeEncryptedData',
                      method: 'POST',
                      header: { 'content-type': 'application/x-www-form-urlencoded' }
                      data: {
                        'type': "userInfo"
                        sessionId: result.sessionId,//wx.getStorageSync('sessionId'),
                        encryptedData: userInfoRes.encryptedData,
                        iv: userInfoRes.iv
                      },
                      success:function(json){
                        console.log('Data decrypted:', json.data);
                      }
                    });
                  }else{
                    console.log('Storing session failed!' , json);
                  }
                }
              })
            }
          })
        }
      });
    }
  }
```

> The above code is the core code of the entire WeChat login client.

Among them:

1. `wx.getUserProfile` is used to invoke the interface to get user information, which is not directly related to the login behaviour itself, but in order to ensure the correctness of the user information, the encrypted information will be used in the subsequent callbacks after the successful login to get the real user information, and therefore needs to be triggered first.

2. `wx.login` is the login interface of the applet client, where `success` is the callback function after login. In the callback function, we use `wx.request` to send a request to the server-side **/WxOpen/OnLogin** address with the `success` callback parameter `res.code`, which will be used to exchange `code` for a `session_key` using the server-side API and stored on the server-side. At the same time, a temporary user identity token, `SessionId`, is generated and returned to the client.
   After successful callback, use `wx.setStorageSync('sessionId', result.sessionId);` to store the received `SessionId` in the local cache. 3.

3. Although `userInfoRes.rawData` has provided the user information in plaintext, it does not ensure that it is secure (untampered with or complete), so a request needs to be sent to the server to verify its authenticity.

   Continue to use the `wx.request` method to request the **/WxOpen/CheckWxOpenSignature** address, sending the `sessionId` as well as the `userInfoRes.rawData` and `userInfoRes.signature` obtained in step 1, to verify the information's authenticity. After passing the validation, you can use `rawData` directly in the client.

   > Note: Some developers will send the validated `rawData` to the server for safekeeping and consider it valid, **this is a risky practice and should be discarded**, the correct practice is to send `userInfoRes.encryptedData` to the server in the following way (**/WxOpen/DecodeEncryptedData**) as described in the code above. encryptedData`to the server. Because: firstly, there is no way to ensure that`rawData` has not been tampered with from the time it is successfully verified to the time it is sent to the server in plaintext; secondly, the transmission of sensitive user information in plaintext is vulnerable to eavesdropping and stealing, and this practice itself should not appear anywhere in the entire project.

4. After authenticity (signature) verification, continue to use `wx.request` to request the server's **/WxOpen/DecodeEncryptedData** address, sending the `sessionId` and the `userInfoRes.encryptedData` and `userInfoRes.encryptedData` you got in step 1, and `userInfoRes.encryptedData`. userInfoRes.iv`, the server side will decrypt `encryptedData` to get the user information and store it.

> Reference document for this project:
>
> Senparc.Weixin.**_WxOpen.AppDemo/app.js_**

## Server Side - OnLogin

The server side **/WxOpen/OnLogin** code is as follows:

```cs
[HttpPost]
public ActionResult OnLogin(string code)
   {
    try
    {
        var jsonResult = SnsApi.JsCode2Json(WxOpenAppId, WxOpenAppSecret, code);
        if (jsonResult.errcode == ReturnCode.Request Success)
        {
            //Session["WxOpenUser"] = jsonResult; //Use Session to save login information (not recommended)
            // Use SessionContainer to manage login information (recommended)
            var unionId = "";
            var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

            // Note: SessionKey is sensitive information in production environments and cannot be transferred!
            return Json(new { success = true, msg = "OK", sessionId = sessionBag.Key, sessionKey = sessionBag.SessionKey/* This parameter must not be exposed to the client! This parameter must not be exposed to the client! */ });
        }
        else {
            return Json(new { success = false, msg = jsonResult.errmsg });
        }
    }
    catch (Exception ex)
    {
        return Json(new { success = false, msg = ex.Message });
    }
}
```

> Special note: `SessionKey` is very sensitive information, the above code is just for demonstration, to prove to the client that it has been generated, and should not be passed to the client during actual development!

> Reference files for this project:
>
> /Controllers/**_WxOpenController.cs_**

## Server-side - CheckWxOpenSignature

Server-side **_/WxOpen/CheckWxOpenSignature_** The code is as follows:

```cs
[HttpPost]
public ActionResult CheckWxOpenSignature(string sessionId, string rawData, string signature)
{
    try
    {
        var checkSuccess = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.CheckSignature(sessionId, rawData, signature);
        return Json(new { success = checkSuccess, msg = checkSuccess ? "Signature check success" : "Signature check failure" });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, msg = ex.Message });
    }
}
```

> Reference file for this project:
>
> /Controllers/**_WxOpenController.cs_**

## Server-side - DecodeEncryptedData

Server Side **/WxOpen/DecodeEncryptedData** The code is as follows:

```cs
public async Task<IActionResult> DecodeEncryptedData(string type, string sessionId, string encryptedData, string iv)
{
    DecodeEntityBase decodedEntity = null;

    try
    {
        switch (type.ToUpper())
        {
            case "USERINFO"://wx.getUserInfo()
                decodedEntity = EncryptHelper.DecodeUserInfoBySessionId(
                    sessionId,
                    encryptedData, iv);
                break;
            default:
                break;
        }
    }
    catch (Exception ex)
    {
        WeixinTrace.SendCustomLog("EncryptHelper.DecodeUserInfoBySessionId method error", $@"sessionId: {sessionId}
encryptedData: {encryptedData}
iv: {iv}
sessionKey: { (await SessionContainer.
CheckRegisteredAsync(sessionId)
        ? (await SessionContainer.GetSessionAsync(sessionId)).SessionKey
        : "Unsaved sessionId")}

Exception Message:
{ex.ToString()}
");
    }

    // checkWatermark
    var checkWatermark = false;
    if (decodedEntity ! = null)
    {
        checkWatermark = decodedEntity.CheckWatermark(WxOpenAppId);

        // save user information (optional)
        if (checkWatermark && decodedEntity is DecodedUserInfo decodedUserInfo)
        {
            var sessionBag = await SessionContainer.GetSessionAsync(sessionId);
            if (sessionBag != null)
            {
                await SessionContainer.AddDecodedUserInfoAsync(sessionBag, decodedUserInfo);
            }
        }
    }

    // Note: This is a demo only, please do not pass sensitive information to the client!
    return Json(new
    {
        success = checkWatermark, //decodedEntity = decodedEntity, }
        //decodedEntity = decodedEntity,
        msg = $"Watermark verification: {(checkWatermark ? "Passed" : "Failed")}"
    });
}
```

> Reference file for this project:
>
> /Controllers/**_WxOpenController.cs_**

## Finish

After completing the code, run the server-side program and the applet client, click the [Get Avatar Nickname] button in the client, enter the login page, click the [Get Avatar Nickname] button, and you can see the pop-up system to confirm the authorization dialogue window:

![授权界面](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-03.png)

Click the [Allow] button, you can automatically complete the entire automatic login, user information capture process:

![完成登录和用户信息获取](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-04.png)

At this time, click the [Get Data] button again, you can see the user information that has been captured:

![用户信息](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-05.png)
