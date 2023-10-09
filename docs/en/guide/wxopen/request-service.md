# Applet Request Service

The applet client can use Ajax-like functionality to send requests to the server and get the response information (usually JSON).

## Place the button that triggers the request

After completing the basic preparations for [client-side development], on the page (e.g. **/pages/index/index.wxml**), create a button for triggering the request:

```html
<button
  type="primary"
  bindtap="doRequest"
  hover-class="other-button-hover"
  class="btn-DoRequest"
>
  Getting data
</button>
```

In the above code, `type`, `class` and `hover-class` set the type, regular style and click style of the button respectively, and `bindtap="doRequest"` specifies that after clicking, it will be handled by the `doRequest()` method (function).

> Reference documentation for this project:
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.wxml_**

## Request server methods

The `doRequest()` method is written in the **index.js** file:

```js
// Handling wx.request requests
doRequest:function(){
  var that = this;
  wx.request({
    url: wx.getStorageSync('domainName') + '/WxOpen/RequestData',
    data: { nickName : that.data.userInfo.nickName},
    method: 'POST', // OPTIONS, GET, HEAD, POST, PUT, DELETE, TRACE, CONNECT
    header: { 'content-type':'application/x-www-form-urlencoded'},
    success: function(res){
      // success
      var json = res.data;
      // Modal dialogue
      wx.showModal({
        title: 'Received Message',
        content: json.msg, showCancel:false,
        showCancel:false,
        success: function(res) {
          if (res.confirm) {
            console.log('User clicked OK')
          }
        }
      });
    },
    fail: function() {
      // fail
    },
    complete: function() {
      // complete
    }
  })
},
```

In the above code

1. `wx.request(...) ` method is used to make a request to the server side, similar to **axios.get() / .post()** or **$.ajax()** in other JS frameworks.
2. `url` is used to specify the API address to be sent. Where `wx.getStorageSync('domainName')` is used to flexibly specify the domain name of the development or production environment (see the **app.js** file in this directory).
3. data" is used to store the data to be submitted, here we get `userInfo.nickName` from the local data centre, when the user is logged in, we can get the **WeChat nickname**, otherwise it is **undefined**. 4.
4. method` is the name of the requested method, currently POST submission is used. 5.
5. `header` is a parameter in the current request header, which can usually also be used to provide user authentication tokens in JWT mode. 6.
6. `success` is the callback after a successful response (200) to the current request. the sample code shows that after receiving the success message a modal dialogue box will pop up to show the return content. click the [OK] button. the console will output the relevant logs.
7. The "Fail" and "Finish" methods are used to handle the failed request and the unified operation after the whole request is completed respectively.

> Reference documentation for this project:
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.js_**

## Server-side interfaces

The server side can use various methods that can accept requests, such as pages, Actions in MVC, Web Api, middleware, and even aspx, ashx, and so on. The above request address is used in the local environment.

The above request address for local environment is **https://localhost:44367/WxOpen/RequestData**, we create an Action of RequestData under WxOpenController to receive the request:

```cs
[HttpPost]
public ActionResult RequestData(string nickName)
{
    var data = new
    {
        msg = string.Format("Server time: {0}, Nickname: {1}", SystemTime.Now.LocalDateTime, nickName)
    };
    return Json(data);
}
```

> Reference file for this project:
>
> /Controllers/**_WxOpenController.cs_**

## Test

Click the [Get Data] button:

![发送请求](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-request-01.png)

Since you are not currently logged in, nicknames are not available, so you can display them after logging in.

> Tip: For login operations, see the [Login] tab.

Click the [OK] button:

![点击确定按钮](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-request-02.png)
