# 小程序请求服务

小程序客户端可以使用类似 Ajax 的功能对服务器端发送请求，并且获取响应信息（一般为 JSON）。

## 放置触发请求的按钮

在完成【客户端开发】的基本准备工作后，在页面上（如 **/pages/index/index.wxml**），创建一个按钮，用于触发请求：

```xml
<button type="primary" bindtap="doRequest"
hover-class="other-button-hover" class="btn-DoRequest">
获取数据
</button>
```

上述代码中，`type`、`class`、`hover-class` 分别设置了按钮的类型、常规样式、点击样式，`bindtap="doRequest"` 指定了点击之后，由 `doRequest()` 方法（function）进行处理。

> 本项目参考文件：
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.wxml_**

## 请求服务器方法

`doRequest() `方法写在 **index.js** 文件中：

```js
//处理wx.request请求
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
      //模组对话框
      wx.showModal({
        title: '收到消息',
        content: json.msg,
        showCancel:false,
        success: function(res) {
          if (res.confirm) {
            console.log('用户点击确定')
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

上述代码中：

1. `wx.request(...)` 方法用于向服务器端发起请求，类似其他 JS 框架中的 **axios.get() / .post()** 或 **$.ajax()** 等。
2. `url` 用于指定需要发送到的 API 地址。其中，`wx.getStorageSync('domainName')` 用于灵活指定开发环境或生产环境的域名（见本目录 **app.js** 文件。
3. `data` 用于存放需要提交的数据，此处我们从本地数据中心取 `userInfo.nickName`，当用户登陆后，即可取到**微信昵称**，否则为 **undefined**。
4. `method` 为请求的方法名称，当前使用 POST 方式提交。
5. `header` 用于指定当前请求 Header 中的参数，通常也可以在 JWT 模式中提供用于用户身份验证的 Token。
6. `success` 为当前请求成功响应（200）后的回调，示例中的代码展示了当收到成功信息后，弹出一个模组对话框，显示返回的内容，并在点击【确定】按钮后，在控制台输出相关日志。
7. `fail` 和 `complete` 方法分别用于处理失败的请求，以及整个请求完成后的统一操作。

> 本项目参考文件：
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.js_**

## 服务器端接口

服务器端可以使用各类能够接受请求的方式，如页面、MVC 中的 Action、Web Api、中间件（Middleware），甚至 aspx、ashx 等。

上述请求地址在本地环境下为：**https://localhost:44367/WxOpen/RequestData**，我们在 WxOpenController 下面创建一个 RequestData 的 Action 用于接收请求：

```cs
[HttpPost]
public ActionResult RequestData(string nickName)
{
    var data = new
    {
        msg = string.Format("服务器时间：{0}，昵称：{1}", SystemTime.Now.LocalDateTime, nickName)
    };
    return Json(data);
}
```

> 本项目参考文件：
>
> /Controllers/**_WxOpenController.cs_**

## 测试

点击【获取数据】按钮：

![发送请求](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-request-01.png)

由于当前没有登录，所以昵称无法获取到，登陆后即可显示昵称。

> 提示：登录操作请见【登录】标签。

点击【确定】按钮：

![点击确定按钮](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-request-02.png)
