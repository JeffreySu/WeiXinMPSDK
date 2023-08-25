# Native Payment

Native payment is used for offline (or outside of WeChat environment) payment, by scanning the QR code through WeChat, which will evoke the personal WeChat payment to complete the payment process.

There are many controls to generate QR code, take [ZXing.Net](https://www.nuget.org/packages/ZXing.Net) as an example, create the method in `TenPayV3Controller`:

```cs
/// <summary>
/// Native Payment Model 1
/// </summary
/// <returns></returns>
public ActionResult Native()
{
    try
    {
        RequestHandler nativeHandler = new RequestHandler(null); string timeStamp = TenPayV3Util.
        string timeStamp = TenPayV3Util.GetTimestamp(); string nonceStr = TenPayV3Util.
        string nonceStr = TenPayV3Util.GetNoncestr();

        // Product Id, user defined
        string productId = SystemTime.Now.ToString("yyyyMMddHHmmss");

        nativeHandler.SetParameter("appid", TenPayV3Info.AppId);
        nativeHandler.SetParameter("mch_id", TenPayV3Info.MchId);
        nativeHandler.SetParameter("time_stamp", timeStamp);
        nativeHandler.SetParameter("nonce_str", nonceStr);
        nativeHandler.SetParameter("product_id", productId);
        string sign = nativeHandler.CreateMd5Sign("key", TenPayV3Info.Key);

        var url = TenPayOldV3.NativePay(TenPayV3Info.AppId, timeStamp, TenPayV3Info.MchId, nonceStr, productId, sign);

        BitMatrix bitMatrix;
        bitMatrix = new MultiFormatWriter().encode(url, BarcodeFormat.QR_CODE, 600, 600); var bw = new ZXing.
        var bw = new ZXing.BarcodeWriterPixelData();

        var pixelData = bw.Write(bitMatrix);
        var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

        var fileStream = new MemoryStream();

        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging. ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
       try
        {
            // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
        }
        finally
        {
            bitmap.UnlockBits(bitmapData);
        }
        bitmap.Save(_fileStream, System.Drawing.Imaging.ImageFormat.Png);
        _fileStream.Seek(0, SeekOrigin.Begin);

        return File(_fileStream, "image/png");
    }
    catch (Exception ex)
    {
        SenparcTrace.SendCustomLog("TenPayV3.Native Execution error", ex.Message);
        Message; SenparcTrace.BaseExceptionLog(ex); throw;
    }

}
```

> Reference file for this project:
>
> /Controllers/**_TenPayV3Controller.cs_**

The above process will automatically generate the payment QR code corresponding to the specified merchant and specified product (productId), and the front-end HTML call is as follows:

```HTML
<img src="/TenPayV3/Native" alt="Scanning code for payment" />
```

After the user sweeps the code to complete the payment, WeChat server will automatically request the callback address, such as /TenPayV3/NativeNotifyUrl, the code is as follows:

```cs
public ActionResult NativeNotifyUrl()
{
    ResponseHandler resHandler = new ResponseHandler(null);

    // Return the request to WeChat
    RequestHandler res = new RequestHandler(null); //

    string openId = resHandler.GetParameter("openid"); //Return to WeChat.
    string productId = resHandler.GetParameter("product_id");

    if (openId == null || productId == null)
    {
        res.SetParameter("return_code", "FAIL");
        res.SetParameter("return_msg", "callback data exception");;
    }

    // Create the payment response object
    //RequestHandler packageReqHandler = new RequestHandler(null);

    var sp_billno = SystemTime.Now.ToString("HHmmss") + TenPayV3Util.BuildRandomStr(26); // up to 32 bits
    var nonceStr = TenPayV3Util.GetNoncestr();

    var xmlDataInfo = new TenPayV3UnifiedorderRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, "test", sp_billno, 1, HttpContext. UserHostAddress()? .ToString(), TenPayV3Info.TenPayV3Notify, TenPay.TenPayV3Type.JSAPI, openId, TenPayV3Info.Key, nonceStr);

    try
    {
        // Call the Unified Order Interface
        var result = TenPayOldV3.Unifiedorder(xmlDataInfo);

        // Create an answer message to return to WeChat
        res.SetParameter("return_code", result.return_code);
        res.SetParameter("return_msg", result.return_msg ?? "OK");
        res.SetParameter("appid", result.appid);
        res.SetParameter("mch_id", result.mch_id);
        res.SetParameter("nonce_str", result.nonce_str);
        res.SetParameter("prepay_id", result.prepay_id);
        res.SetParameter("result_code", result.result_code);
        res.SetParameter("err_code_des", "OK");
        string nativeReqSign = res.CreateMd5Sign("key", TenPayV3Info.Key);

        res.SetParameter("sign", nativeReqSign);
    }
    catch (Exception)
    {
        res.SetParameter("return_code", "FAIL"); res.
        res.SetParameter("return_msg", "Uniform order failure");
    }

    return Content(res.ParseXML());
}
```

> Reference file for this project:
>
> /Controllers/**_TenPayV3Controller.cs_**

> Tip:
>
> The callback address setting location for Native payment is located at: WeChat Pay backend > Product Centre > Development Configuration > Native Payment Callback Link.
>
> ![Native 支付回调链接设置 ](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/native-setting-01.png)
