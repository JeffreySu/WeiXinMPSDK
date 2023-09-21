# Native 支付

Native 支付用于线下（或微信环境以外）的支付，通过微信扫描二维码，唤起个人微信支付完成支付过程。

生成二维码的控件很多，以 [ZXing.Net](https://www.nuget.org/packages/ZXing.Net) 为例，在 `TenPayV3Controller` 中创建方法：

```cs
/// <summary>
/// 原生支付 模式一
/// </summary>
/// <returns></returns>
public ActionResult Native()
{
    try
    {
        RequestHandler nativeHandler = new RequestHandler(null);
        string timeStamp = TenPayV3Util.GetTimestamp();
        string nonceStr = TenPayV3Util.GetNoncestr();

        //商品Id，用户自行定义
        string productId = SystemTime.Now.ToString("yyyyMMddHHmmss");

        nativeHandler.SetParameter("appid", TenPayV3Info.AppId);
        nativeHandler.SetParameter("mch_id", TenPayV3Info.MchId);
        nativeHandler.SetParameter("time_stamp", timeStamp);
        nativeHandler.SetParameter("nonce_str", nonceStr);
        nativeHandler.SetParameter("product_id", productId);
        string sign = nativeHandler.CreateMd5Sign("key", TenPayV3Info.Key);

        var url = TenPayOldV3.NativePay(TenPayV3Info.AppId, timeStamp, TenPayV3Info.MchId, nonceStr, productId, sign);

        BitMatrix bitMatrix;
        bitMatrix = new MultiFormatWriter().encode(url, BarcodeFormat.QR_CODE, 600, 600);
        var bw = new ZXing.BarcodeWriterPixelData();

        var pixelData = bw.Write(bitMatrix);
        var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

        var fileStream = new MemoryStream();

        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
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
        SenparcTrace.SendCustomLog("TenPayV3.Native 执行出错", ex.Message);
        SenparcTrace.BaseExceptionLog(ex);

        throw;
    }
}
```

> 本项目参考文件：
>
> /Controllers/**_TenPayV3Controller.cs_**

上述过程将自动生成对应于指定商户、指定商品（productId）的付款二维码，前端 HTML 调用方式如下：

```HTML
<img src="/TenPayV3/Native" alt="扫码付款" />
```

用户扫码完成支付后，微信服务器会自动请求回调地址，如 /TenPayV3/NativeNotifyUrl，代码如下：

```cs
public ActionResult NativeNotifyUrl()
{
    ResponseHandler resHandler = new ResponseHandler(null);

    //返回给微信的请求
    RequestHandler res = new RequestHandler(null);

    string openId = resHandler.GetParameter("openid");
    string productId = resHandler.GetParameter("product_id");

    if (openId == null || productId == null)
    {
        res.SetParameter("return_code", "FAIL");
        res.SetParameter("return_msg", "回调数据异常");
    }

    //创建支付应答对象
    //RequestHandler packageReqHandler = new RequestHandler(null);

    var sp_billno = SystemTime.Now.ToString("HHmmss") + TenPayV3Util.BuildRandomStr(26);//最多32位
    var nonceStr = TenPayV3Util.GetNoncestr();

    var xmlDataInfo = new TenPayV3UnifiedorderRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, "test", sp_billno, 1, HttpContext.UserHostAddress()?.ToString(), TenPayV3Info.TenPayV3Notify, TenPay.TenPayV3Type.JSAPI, openId, TenPayV3Info.Key, nonceStr);

    try
    {
        //调用统一订单接口
        var result = TenPayOldV3.Unifiedorder(xmlDataInfo);

        //创建应答信息返回给微信
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
        res.SetParameter("return_code", "FAIL");
        res.SetParameter("return_msg", "统一下单失败");
    }

    return Content(res.ParseXML());
}
```

> 本项目参考文件：
>
> /Controllers/TenPayV3Controller.cs

> 提示：
>
> Native 支付的回调地址设置位置位于：微信支付后台 > 产品中心 > 开发配置 > Native 支付回调链接。
>
> ![Native 支付回调链接设置 ](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/native-setting-01.png)Native
