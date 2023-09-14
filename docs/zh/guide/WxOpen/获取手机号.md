# 获取手机号

> 注意：小程序获取手机号的接口，进行过一次升级，先前在客户端直接获取手机号的方式已经被淘汰（示例中左侧【获取手机号】按钮），目前最新的接口是使用 code 到服务器端后台获取手机号（示例中右侧【获取手机号（Code）】按钮。
>
> ![入口页面](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-phone-01.png)入口页面
>
> 当用户点击后，系统进行授权提示：
>
> ![授权手机号](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-phone-02.png)授权手机号
>
> 点击【允许】按钮，通过访问后端接口，获取到手机号：
>
> ![授权手机号](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-phone-03.png)授权手机号

## 客户端

在 Index.wxml 文件中放置按钮：

```XML
<button open-type="getPhoneNumber" bindgetphonenumber="getUserPhoneNumber" type="primary"
        class="btn-DoRequest" hover-class="other-button-hover" >获取手机号（code）</button>
```

> 本项目参考文件：
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.wxml_**

上述代码中，`open-type="getPhoneNumber"` 指定了当前按钮获取手机号的用途，`bindgetphonenumber="getUserPhoneNumber"` 指定了处理方法为 `getUserPhoneNumber`（对应于 .js 文件中）：

```JS
getUserPhoneNumber:function(e){
    wx.request({
      url: wx.getStorageSync('domainName') + '/WxOpen/GetUserPhoneNumber?code=' + e.detail.code,
      success: function (res) {
        // success
        var json = res.data;

        if(!json.success){
          wx.showModal({
            title: '解密过程发生异常',
            content: json.msg,
            showCancel: false
          });
          return;
        }

        //模组对话框
        var phoneNumberData = json.phoneInfo;
        var msg = '手机号：' + phoneNumberData.phoneNumber+
          '\r\n手机号（不带区号）：' + phoneNumberData.purePhoneNumber+
          '\r\n区号（国别号）' + phoneNumberData.countryCode+
          '\r\n水印信息：' + JSON.stringify(phoneNumberData.watermark);

        wx.showModal({
          title: '收到服务器端通过 code 获取的手机号信息',
          content: msg,
          showCancel: false
        });
      }
    })
  }
```

当用户点击按钮，并授权手机号后，就会触发上述方法，通过 `e` 参数提供的 `e.code`，将其提交给服务器后台 **/WxOpen/GetUserPhoneNumber** 地址，后台将使用 `code` 换取用户的手机号，然后进行储存或返回给前端。上述代码的模组对话框只是演示作用，实际项目中一般不需要再次弹出信息。

> 本项目参考文件：
>
> Senparc.Weixin.WxOpen.AppDemo/**_/pages/index/index.js_**

## 后端代码 - GetUserPhoneNumber

```cs
public async Task GetUserPhoneNumber(string code)
{
    try
    {
        var result = await BusinessApi.GetUserPhoneNumberAsync(WxOpenAppId, code);
        return Json(new { success = true, phoneInfo = result.phone_info });
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
