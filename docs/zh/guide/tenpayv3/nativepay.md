# Native 支付

## Native 支付

Native 支付用于线下（或微信环境以外）的支付，通过微信扫描二维码，唤起个人微信支付完成支付过程。

生成二维码的控件很多，以 [ZXing.Net](https://www.nuget.org/packages/ZXing.Net) 为例，在 `TenPayApiV3Controller` 中创建方法：

```cs
/// <summary>
/// 使用 Native 支付
/// </summary>
/// <param name="productId"></param>
/// <param name="hc"></param>
/// <returns></returns>
public async Task<IActionResult> NativePayCode(int productId, int hc)
{
    var products = ProductModel.GetFakeProductList();
    var product = products.FirstOrDefault(z => z.Id == productId);
    if (product == null || product.GetHashCode() != hc)
    {
        return Content("商品信息不存在，或非法进入！2004");
    }

    //使用 Native 支付，输出二维码并展示
    MemoryStream fileStream = null;//输出图片的URL
    var price = (int)(product.Price * 100);
    var name = product.Name + " - 微信支付 V3 - Native 支付";
    var sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                    TenPayV3Util.BuildRandomStr(6));

    var notifyUrl = TenPayV3Info.TenPayV3Notify.Replace("/TenpayApiV3/", "/TenpayApiV3/");

    TransactionsRequestData requestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, name, sp_billno, new TenpayDateTime(DateTime.Now.AddHours(1)), null, notifyUrl, null, new() { currency = "CNY", total = price }, null, null, null, null);

    BasePayApis basePayApis = new BasePayApis();
    var result = await basePayApis.NativeAsync(requestData);
    //进行安全签名验证
    if (result.VerifySignSuccess == true)
    {
        fileStream = QrCodeHelper.GerQrCodeStream(result.code_url);
    }
    else
    {
        fileStream = QrCodeHelper.GetTextImageStream("Native Pay 未能通过签名验证，无法显示二维码");
    }
    return File(fileStream, "image/png");
}
```

> 本项目参考文件：
>
> /Controllers/TenPayApiV3Controller.cs

上述过程将自动生成对应于指定商户、指定商品（productId）的付款二维码，前端 HTML 调用方式如下：

```HTML
<img src="/TenpayApiV3/NativePayCode" alt="扫码付款" />
```

用户扫码完成支付后，微信服务器会自动请求回调地址，如 /TenpayApiV3/NativeNotifyUrl，代码如下：

```
//待补充
```

> 本项目参考文件：
>
> /Controllers/**_TenPayApiV3Controller.cs_**

> 提示：
>
> Native 支付的回调地址设置位置位于：微信支付后台 > 产品中心 > 开发配置 > Native 支付回调链接。
>
> ![Native 支付回调链接设置 ](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/native-setting-01.png)Native 支付回调链接设置
