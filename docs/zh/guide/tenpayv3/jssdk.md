# JSAPI

## JSAPI 支付

在微信公众号网页中使用微信支付，必须借助 JSAPI，完成支付流程。

首先，需要创建一个商品页面，并在页面上提供一个下订单的入口，例如：一键购买。
（具体实现此处略）

当前示例中，提供了 3 个关键的页面：ProductList（商品列表）、ProductItem（商品详情） 和 JsApi（JSAPI 订单支付）。

## ProductList 商品列表

**后端**

参考代码：`TenPayApiV3Controller` 下的 ProductList() 方法。

```cs
public ActionResult ProductList()
{
    var products = ProductModel.GetFakeProductList();
    return View(products);
}
```

> 本项目参考文件：
>
> /**_Controllers/TenPayApiV3Controller.cs_**

**前端**

> 本项目参考文件：
>
> **_/Views/TenPayApiV3/ProductItem.cshtml_**

**效果**

[打开预览](https://sdk.weixin.senparc.com/TenpayApiV3/ProductList)

![商品列表](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-01.png)

## ProductItem 商品详情

**后端**

参考代码：`TenPayApiV3Controller` 下的 ProductItem() 方法。

```cs
public ActionResult ProductItem(int productId, int hc)
{
    var products = ProductModel.GetFakeProductList();
    var product = products.FirstOrDefault(z => z.Id == productId);
    if (product == null || product.GetHashCode() != hc)
    {
        return Content("商品信息不存在，或非法进入！2003");
    }

    //判断是否正在微信端
    if (Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser(HttpContext))
    {
        //正在微信端，直接跳转到微信支付页面
        return RedirectToAction("JsApi", new { productId = productId, hc = hc });
    }
    else
    {
        //在PC端打开，提供二维码扫描进行支付
        return View(product);
    }
}
```

> 说明：上述代码使用 `SideInWeixinBrowser()` 方法对当前页面的运行环境做了判断，如果是在微信内打开，则直接进入 JsApi 支付页面（见下放 JsApi 的介绍），如果在非微信内部打开（如 PC），则展示商品详情，并提供多种支付方式的选择。

> 本项目参考文件：
>
> /**_Controllers/TenPayApiV3Controller.cs_**

**前端**

> 本项目参考文件：
>
> **_/Views/TenPayApiV3/ProductItem.cshtml_**

**效果**

从 [商品列表](https://sdk.weixin.senparc.com/TenPayApiV3/ProductList) 中选择一个商品点击，如果在 PC 端，则可以看到详情页面：

![商品列表](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-02.png)

上述的**支付方式二：“扫一扫”支付**下方有一个根据所选商品自动生成的二维码，使用手机微信扫一扫，即可进入对应的商品订单页面（即 JsApi 订单页面）。

## JSAPI 订单支付页面

**后端**

用户点击下单按钮后，需要在后台生成一个预支付订单并在页面上登记，代码请参考 `TenPayApiV3Controller.JsApi()`

```cs
[CustomOAuth(null, "/TenpayApiV3/OAuthCallback")]
public ActionResult JsApi(int productId, int hc)
{
    try
    {
        //获取产品信息
        //...

        //调用 JsApi 获取预支付信息
        //请求信息
        TransactionsRequestData jsApiRequestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, name,
            sp_billno, new TenpayDateTime(DateTime.Now.AddHours(1), false), null, notifyUrl, null,
            new() { currency = "CNY", total = price }, new(openId), null, null, null);

        //请求接口
        var basePayApis2 = new Senparc.Weixin.TenPayV3.TenPayHttpClient.BasePayApis2(_httpClient,
            _tenpayV3Setting);
        var result = await basePayApis2.JsApiAsync(jsApiRequestData);

        if (result.VerifySignSuccess != true)
        {
            throw new WeixinException("获取 prepay_id 结果校验出错！");
        }

        //获取 UI 信息包
        var jsApiUiPackage = TenPaySignHelper.GetJsApiUiPackage(TenPayV3Info.AppId, result.prepay_id);
        ViewData["jsApiUiPackage"] = jsApiUiPackage;

        //其他逻辑
        //...

        return View();
    }
    catch (Exception ex)
    {
        //...
    }
}
```

> 本项目参考文件：
>
> /**_Controllers/TenPayApiV3Controller.cs_**

上述代码中，传入的 `productId` 参数是商品的编号，此处作为 Sample 演示，是从内存中模拟列表并查询，实际项目中，商品信息一般存储在数据库中，根据 `productId` 从数据中查找商品数据；`hc` 函数是为了确保当前内存信息的有效性而设置的对应商品信息的 HashCode，实际开发项目中无需使用，可忽略。

此过程中，最关键的代码是：`var result = TenPayOldV3.Unifiedorder(xmlDataInfo)`，`result.prepay_id` 即“预支付 ID”，前端页面必须凭借 prepay_id 才能让手机端唤起微信支付。此时，当前订单编号已经在微信支付后台进行了注册。

注意：该方法使用了 **[CustomOAuth]** 特性，用于自动使用微信公众号的 OAuth 功能识别用户身份，此功能属于公众号范畴，不在这里展开。

**前端**

前端的关键操作是当用户点击“支付”按钮后，执行 JS 代码：

```cs
// 当微信内置浏览器完成内部初始化后会触发WeixinJSBridgeReady事件。
document.addEventListener('WeixinJSBridgeReady'). function onBridgeReady() {
    //公众号支付
    jQuery('a#getBrandWCPayRequest').click(function (e) {
        WeixinJSBridge.invoke('getBrandWCPayRequest', {
            "appId": "@jsApiUiPackage.AppId", //公众号名称，由商户传入
            "timeStamp": "@jsApiUiPackage.Timestamp", //时间戳
            "nonceStr": "@jsApiUiPackage.NonceStr", //随机串
            "package": "@Html.Raw(jsApiUiPackage.PrepayIdPackage)",//扩展包
            "signType": "RSA", //微信V3签名方式:RSA
            "paySign": "@Html.Raw(jsApiUiPackage.Signature)" //微信签名
        }, function (res) {

            //alert(JSON.stringify(res));

            if (res.err_msg == "get_brand_wcpay_request:ok") {
                if (confirm('支付成功！点击“确定”进入退款流程测试。')) {
                    location.href = '/Docs/TenPayV3/TenpayApiV3/Refund';
                }
                //console.log(JSON.stringify(res));
            }else{
                alert(JSON.stringify(res));
            }
            // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
            //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
        });

    });
```

> 注意：微信支付 ApiV2 和 ApiV3 在订单接口有完全不同的区别，如果升级请留意！

> 本项目参考文件：
>
> /**_Views/TenpayApiV3/JsApi.cshtml_**

**效果**

![订单支付页面](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-03.jpg)

![点击唤起支付](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-04.jpg)

![支付成功](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-05.jpg)
