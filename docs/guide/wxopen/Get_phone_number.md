# Get phone number

Note: The interface of the applet to get the mobile phone number has been upgraded, the previous way to get the mobile phone number directly in the client has been eliminated (the left side of the example [Get Mobile Phone Number] button), and the latest interface is to use the code to get the mobile phone number in the server background the right side of the example [Get Mobile Phone Number (Code)] button.

![入口页面](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-phone-01.png)

When the user clicks on it, the system carries out an authorisation prompt:

![授权手机号](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-phone-02.png)

Click the [Allow] button to get the mobile phone number by accessing the back-end interface:

![授权手机号](https://sdk.weixin.senparc.com/Docs/WxOpen/images/use-phone-03.png)

## Client

Place buttons in the Index.wxml file:

```xml
<button
  open-type="getPhoneNumber"
  bindgetphonenumber="getUserPhoneNumber"
  type="primary"
  class="btn-DoRequest"
  hover-class="other-button-hover"
>
  Get Mobile Number（code）
</button>
```

> Reference file for this project:
>
> Senparc.Weixin.WxOpen.AppDemo/**_pages/index/index.wxml_**

In the above code, `open-type="getPhoneNumber"` specifies the use of the current button to get the phone number, and `bindgetphonenumber="getUserPhoneNumber"` specifies that the handling method is `getUserPhoneNumber` (corresponding to the .js file) :

```js
getUserPhoneNumber:function(e){
    wx.request({
      url: wx.getStorageSync('domainName') + '/WxOpen/GetUserPhoneNumber?code=' + e.detail.code,
      success: function (res) {
        // Success
        var json = res.data;
​        if(!json.success){
​          wx.showModal({
​            title: 'Exception occurred during decryption',
​            content: json.msg,
​            showCancel: false
​          });
​          return;
​        }
​        // Module Dialog
​        var phoneNumberData = json.phoneInfo;
​        var msg = 'Phone Number: ' + phoneNumberData.phoneNumber+
​          'Mobile Phone Number (without area code): ' + phoneNumberData.purePhoneNumber+
​          '(Area code (country code)' + phoneNumberData.countryCode+
​          'Watermark information:' + JSON.stringify(phoneNumberData.watermark);

​        wx.showModal({
​          title: 'Receive server-side phone number information via code',
​          content: msg,
​          showCancel: false
​       });
​      }
​    })
  }
```

When the user clicks the button and authorises the phone number, the above method will be triggered and submitted to the server backend via `e.code` provided by the `e` parameter of the **/WxOpen/GetUserPhoneNumber** address, which exchanges the user's phone number with the `code` and stores or returns it to the frontend. The modal dialogue box in the above code is for demonstration purposes only, there is no need to pop up this message again in the actual project.

> Reference file for this project:
> Senparc.Weixin.WxOpen.AppDemo/**_/pages/index/index.js_**

## Backend code - GetUserPhoneNumber

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

> Reference documentation for this project:
> Controllers/**_WxOpenController.cs_**
