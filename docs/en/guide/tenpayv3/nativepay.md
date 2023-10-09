# Native Payments

## Native Payment

Native payment is used for offline (or outside of WeChat environment) payment, by scanning the QR code through WeChat, it will evoke the personal WeChat payment to complete the payment process.

There are many controls to generate QR code, take [ZXing.Net](https://www.nuget.org/packages/ZXing.Net) as an example, create the method in `TenPayApiV3Controller`:

```cs
/// <summary
/// Using Native Payments
/// </summary
/// <param name="productId"></param>
/// <param name="hc"></param>
/// <returns></returns
public async Task<IActionResult> NativePayCode(int productId, int hc)
{
    var products = ProductModel.GetFakeProductList();
    var product = products.FirstOrDefault(z => z.Id == productId); var product = products.
    if (product == null || product.GetHashCode() ! = GetHashCode() !
    hc) {
        return Content("Product information doesn't exist or was entered illegally! 2004");
    }

    // Use Native payment, output QR code and display it
    MemoryStream fileStream = null; // output the URL of the image
    var price = (int)(product.Price * 100); var name = product.Name + "); //Output the URL of the image.
    var name = product.Name + " - WeChat Pay V3 - Native Payment";
    var sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10 digits*/, SystemTime.Now.ToString("yyyyyMMddHHmmss"),
                    TenPayV3Util.BuildRandomStr(6)); var notifyUrl = TenPayV3Util.

    var notifyUrl = TenPayV3Info.TenPayV3Notify.Replace("/TenpayApiV3/", "/TenpayApiV3/");

    TransactionsRequestData requestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, name, sp_billno, new TenpayDateTime(DateTime.Now. AddHours(1)), null, notifyUrl, null, new() { currency = "CNY", total = price }, null, null, null, null);

    BasePayApis basePayApis = new BasePayApis(); var result = await basePayApis
    var result = await basePayApis.NativeAsync(requestData);
    // Perform secure signature verification
    if (result.VerifySignSuccess == true)
    {
        fileStream = QrCodeHelper.GerQrCodeStream(result.code_url);
    }
    else
    {
        fileStream = QrCodeHelper.GetTextImageStream("Native Pay failed signature verification, unable to display QR code");
    }
    return File(fileStream, "image/png");
}
```

> Reference file for this project:
>
> /Controllers/TenPayApiV3Controller.cs

The above process will automatically generate the payment QR code corresponding to the specified merchant and specified product (productId), and the front-end HTML call is as follows:

```html
<img src="/TenpayApiV3/NativePayCode" alt="Scan code to pay" />
```

After the user sweeps the code to complete the payment, WeChat server will automatically request the callback address, such as /TenpayApiV3/NativeNotifyUrl, with the following code:

```
//to be added
```

> Reference file for this project:
>
> /Controllers/TenPayApiV3Controller.cs

> Tip:
>
> The callback address setting location for Native payment is located at: WeChat Payment Backend > Product Centre > Development Configuration > Native Payment Callback Link.
>
> ![Native 支付回调链接设置 ](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/native-setting-01.png)
