# 登录

## 简介

大部分的小程序都需要识别用的身份信息（OpenId）以及获取用户的头像、昵称等信息为用户提供身份识别、个性化信息展示的服务。此时就需要使用到小程序登录接口。

## 客户端 - 进入登录页

用户的登录是在小程序端发起的，一般而言可以做一个独立的页面用于放置说明及登录按钮。首先，在入口网页放置一个入口按钮：

```HTML
<button bindtap="getUserInfo"> 获取头像昵称 </button>
```

点击按钮后，触发 .js 文件中的 `getUserInfo()` 方法：

```JS
  getUserInfo: function(){
    wx.navigateTo({
      url: '../Login/Login',
    })
  }
```

此按钮将引导页面跳转到独立的 Login 页面。

![跳转到登录页](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-01.png)跳转到登录页

> 本项目参考文件： Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.wxml_** Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.js_**

## 客户端 - 登录页

登录页包含系统登录展示、隐私协议等内容，并包含一个触发最终小程序客户端登录事件的按钮：

```XML
<!--pages/Login/Login.wxml-->
<view class="auth-notice">
      <text class="auth-notice">您好，</text><open-data type="userNickName" id="login-nickname"></open-data>，
      <text class="auth-notice">当前小程序为 Senparc.Weixin SDK 的功能体验小程序，包括了订阅消息、获取用户授权信息、手机号、WebSocket、客服消息等演示内容，大部分内容需要授权后进行，点击【获取头像昵称】按钮进行授权，才能进入测试页面。</text>
      <text class="auth-notice">如果您不希望授权，请直接关闭此页面。</text>
      <button class="auth-btn" bindtap="getUserInfo"> 获取头像昵称 </button>
</view>
```

![登录页面](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-02.png)

> 本项目参考文件：
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/Login/Login.wxml_**

【获取头像昵称】按钮绑定了方法 `getUserInfo()`，在 **Login.js** 中添加：

```JS
var app = getApp()
getUserInfo: function (e) {
    var that = this;
    app.getUserInfo(e, function(userInfo){
      app.globalData.userInfo = userInfo
      that.setData({
        userInfo: userInfo,
        hasUserInfo: true
      });
      wx.navigateTo({
        url: '../index/index',
      })
    });
  }
```

> 本项目参考文件：
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/Login/Login.js_**

上述代码中，使用 `var app = getApp()` 引入了全局方法（代码在根目录 **app.js**中），其中，`app.getUserInfo()` 方法代码如下（为了更加贴近实际使用场景，我们将 **登录** + **获取用户信息** 放在连贯的代码中展示）：

```JS
getUserInfo:function(cb,callback){
    var that = this
    if(this.globalData.userInfo){
      typeof cb == "function" && cb(this.globalData.userInfo)
    }else{
    //获取userInfo并校验
    console.log('准备调用 wx.getUserProfile');
    wx.getUserProfile({
      desc: '用于完善会员资料', // 声明获取用户个人信息后的用途，后续会展示在弹窗中，请谨慎填写
      success: function (userInfoRes) {
        console.log('get getUserProfile', userInfoRes);
        that.globalData.userInfo = userInfoRes.userInfo
        typeof cb == "function" && cb(that.globalData.userInfo)
        typeof callback == "function" && callback(userInfoRes.userInfo)

        //调用登录接口
        wx.login({
            success: function (res) {
              //换取openid & session_key
              wx.request({
                url: wx.getStorageSync('domainName') + '/WxOpen/OnLogin',
                method: 'POST',
                header: { 'content-type': 'application/x-www-form-urlencoded' },
                data: {
                  code: res.code
                },
                success:function(json){
                  console.log('wx.login - request-/WxOpen/OnLogin Result:', json);
                  var result = json.data;
                  if(result.success)
                  {
                    wx.setStorageSync('sessionId', result.sessionId);
                    //校验
                    wx.request({
                      url: wx.getStorageSync('domainName') + '/WxOpen/CheckWxOpenSignature',
                      method: 'POST',
                      header: { 'content-type': 'application/x-www-form-urlencoded' },
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

                    //解密数据（建议放到校验success回调函数中，此处仅为演示）
                    wx.request({
                      url: wx.getStorageSync('domainName') + '/WxOpen/DecodeEncryptedData',
                      method: 'POST',
                      header: { 'content-type': 'application/x-www-form-urlencoded' },
                      data: {
                        'type':"userInfo",
                        sessionId: result.sessionId,//wx.getStorageSync('sessionId'),
                        encryptedData: userInfoRes.encryptedData,
                        iv: userInfoRes.iv
                      },
                      success:function(json){
                        console.log('数据解密：', json.data);
                      }
                    });
                  }else{
                    console.log('储存session失败！',json);
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

> 上述代码是整个微信登录客户端的核心代码。

其中：

1. `wx.getUserProfile` 用于调起获取用户信息的接口，此接口本身和登录行为本身无直接联系，但是为了保证用户信息的正确性，后续需要在登录成功后的回调中使用到其中的加密信息来获取真实的用户信息，因此需要首先触发。

2. `wx.login` 是小程序客户端的登录接口，其中 `success` 为登录自后的回调函数。在回调函数中，我们使用 `wx.request` 向服务器端 **/WxOpen/OnLogin** 地址发送一条请求，带上 `success` 回调的参数 `res.code`，其中将使用 `code` 利用服务器端的 API 换取 `session_key`，并储存在服务器端，同时生成临时的用户身份标记 `SessionId` 并返回给客户端。
   成功回调后，再使用 `wx.setStorageSync('sessionId', result.sessionId);` 将收到的 `SessionId` 储存在本地缓存中。

3. 虽然`userInfoRes.rawData` 已经提供了明文的用户信息，但并不能确保其是安全（未经篡改或完整）的，因此需要向服务器发送请求，验证其真实性。

   继续使用`wx.request`方法请求**/WxOpen/CheckWxOpenSignature**地址，发送`sessionId`以及第 1 步中获取到的`userInfoRes.rawData`以及`userInfoRes.signature`，校验信息的真实性。验证通过后，可以在客户端直接使用`rawData`。

   > 注意：有的开发者会把经过验证后的 `rawData` 发送给服务器保存，认为此信息是有效的，**这是具有风险的做法，应当抛弃**，正确的做法是使用上述代码中后续的方式（**/WxOpen/DecodeEncryptedData**）发送 `userInfoRes.encryptedData` 给服务器解密。因为：第一，`rawData` 从被验证成功到明文发送给服务器的过程中无法确保是否被篡改；第二，明文传输用户的敏感信息容易被监听和窃取，这种做法本身不应该出现在整个项目的任何地方。

4. 真实性（签名）验证通过后，继续使用 `wx.request` 请求服务器 **/WxOpen/DecodeEncryptedData** 地址，发送 `sessionId`以及第 1 步中获取到的 `userInfoRes.encryptedData` 以及 `userInfoRes.iv`，服务器端将解密 `encryptedData` 获得用户信息，并储存。

> 本项目参考文件：
>
> Senparc.Weixin.**_WxOpen.AppDemo/app.js_**

## 服务器端 - OnLogin

服务器端 **/WxOpen/OnLogin** 代码如下：

```cs
[HttpPost]
public ActionResult OnLogin(string code)
{
    try
    {
        var jsonResult = SnsApi.JsCode2Json(WxOpenAppId, WxOpenAppSecret, code);
        if (jsonResult.errcode == ReturnCode.请求成功)
        {
            //Session["WxOpenUser"] = jsonResult;//使用Session保存登陆信息（不推荐）
            //使用SessionContainer管理登录信息（推荐）
            var unionId = "";
            var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

            //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
            return Json(new { success = true, msg = "OK", sessionId = sessionBag.Key, sessionKey = sessionBag.SessionKey/* 此参数千万不能暴露给客户端！处仅作演示！ */ });
        }
        else
        {
            return Json(new { success = false, msg = jsonResult.errmsg });
        }
    }
    catch (Exception ex)
    {
        return Json(new { success = false, msg = ex.Message });
    }
}
```

> 特别注意：`SessionKey` 是非常敏感的信息，上述代码只是做演示，向客户端证明已经生成，实际开发过程中不可传递到客户端！

> 本项目参考文件：
>
> /Controllers/**_WxOpenController.cs_**

## 服务器端 - CheckWxOpenSignature

服务器端 **/WxOpen/CheckWxOpenSignature** 代码如下：

```cs
[HttpPost]
public ActionResult CheckWxOpenSignature(string sessionId, string rawData, string signature)
{
    try
    {
        var checkSuccess = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.CheckSignature(sessionId, rawData, signature);
        return Json(new { success = checkSuccess, msg = checkSuccess ? "签名校验成功" : "签名校验失败" });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, msg = ex.Message });
    }
}
```

> 本项目参考文件：
>
> /Controllers/**_WxOpenController.cs_**

## 服务器端 - DecodeEncryptedData

服务器端 **/WxOpen/DecodeEncryptedData** 代码如下：

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
        WeixinTrace.SendCustomLog("EncryptHelper.DecodeUserInfoBySessionId 方法出错",
            $@"sessionId: {sessionId}
encryptedData: {encryptedData}
iv: {iv}
sessionKey: { (await SessionContainer.CheckRegisteredAsync(sessionId)
        ? (await SessionContainer.GetSessionAsync(sessionId)).SessionKey
        : "未保存sessionId")}

异常信息：
{ex.ToString()}
");
    }

    //检验水印
    var checkWatermark = false;
    if (decodedEntity != null)
    {
        checkWatermark = decodedEntity.CheckWatermark(WxOpenAppId);

        //保存用户信息（可选）
        if (checkWatermark && decodedEntity is DecodedUserInfo decodedUserInfo)
        {
            var sessionBag = await SessionContainer.GetSessionAsync(sessionId);
            if (sessionBag != null)
            {
                await SessionContainer.AddDecodedUserInfoAsync(sessionBag, decodedUserInfo);
            }
        }
    }

    //注意：此处仅为演示，敏感信息请勿传递到客户端！
    return Json(new
    {
        success = checkWatermark,
        //decodedEntity = decodedEntity,
        msg = $"水印验证：{(checkWatermark ? "通过" : "不通过")}"
    });
}
```

> 本项目参考文件：
>
> /Controllers/**_WxOpenController.cs_**

## 完成

完成代码后，运行服务器端程序和小程序客户端，在客户端中点击【获取头像昵称】按钮，进入登录页面，点击【获取头像昵称】按钮，即可看到弹出系统确认授权对话窗口：

![授权界面](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-03.png)

点击【允许】按钮，即可自动完成整个自动登录、用户信息抓取过程：

![完成登录和用户信息获取](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-04.png)

此时，再点击【获取数据】按钮，即可看到已经获取到的用户信息：

![用户信息](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-login-05.png)
