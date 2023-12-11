# JSAPI

## JSAPI Payment

To use WeChat payment in WeChat public web page, you must complete the payment process with the help of JSAPI.

First of all, you need to create a product page and provide an entry point to place an order on the page, e.g. one-click purchase.
(implementation omitted)

In the current example, 3 key pages are provided: ProductList, ProductItem and JsApi (JSAPI order payment).

## ProductList ProductList

**Backend**

Reference code: ProductList() method under `TenPayApiV3Controller`.

```cs
public ActionResult ProductList()
{
    var products = ProductModel.GetFakeProductList();
    return View(products);
}
```

> Reference file for this project:
>
> /**_Controllers/TenPayApiV3Controller.cs_**

**Frontend**

> Reference file for this project:
>
> /**_/Views/TenPayApiV3/ProductItem.cshtml_**

**Effects**

[Open Preview](https://sdk.weixin.senparc.com/TenpayApiV3/ProductList)

![商品列表](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-01.png)

## ProductItem Product details

**Backend**

Reference code: `ProductItem() method under TenPayApiV3Controller`.

```cs
public ActionResult ProductItem(int productId, int hc)
{
    var products = ProductModel.GetFakeProductList();
    var product = products.FirstOrDefault(z => z.Id == productId);
    if (product == null || product.GetHashCode() ! = GetHashCode() !
    hc) {
        return Content("Product information doesn't exist or was entered illegally! 2003");
    }

    // Determine if it is being microsoft
    if (Senparc.Weixin.BrowserUtility.BrowserUtility.SideInWeixinBrowser(HttpContext))
    {
        // Being on WeChat side, directly jump to WeChat payment page
        return RedirectToAction("JsApi", new { productId = productId, hc = hc });
    }
    else
    {
        // Open it on PC and provide QR code scanning for payment
        return View(product);
    }

}
```

> Description: The above code uses `SideInWeixinBrowser()` method to judge the running environment of the current page, if it is opened in WeChat, it will directly enter the JsApi payment page (see the introduction of the devolved JsApi), if it is opened in a non-WeChat environment (e.g., PC), it will display the product details and provide a choice of payment methods.

> Reference file for this project:
>
> /**_Controllers/TenPayApiV3Controller.cs_**

**Frontend**

> Reference file for this project:
>
> **_/Views/TenPayApiV3/ProductItem.cshtml_**

**Effects**

Select a product from [Product List](https://sdk.weixin.senparc.com/TenPayApiV3/ProductList) and click on it, you can see the detail page if you are on PC:

![商品列表](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-02.png)

The above **Payment Method 2: "Sweep" Payment** There is a QR code automatically generated according to the selected product at the bottom of the page, use WeChat on your mobile phone to sweep it, then you can enter the corresponding product order page (i.e., JsApi order page).

## JSAPI order payment page

**Backend**

After the user clicks the order button, a prepaid order needs to be generated in the backend and registered on the page, please refer to `TenPayApiV3Controller.JsApi()`

```cs
[CustomOAuth(null, "/TenpayApiV3/OAuthCallback")]
public ActionResult JsApi(int productId, int hc)
{
    try
    {
        //Get the product information
        //...

        // Call JsApi to get prepayment information.
        //Request information
        TransactionsRequestData jsApiRequestData = new(TenPayV3Info.AppId, TenPayV3Info.MchId, name,
            sp_billno, new TenpayDateTime(DateTime.Now.AddHours(1), false), null, notifyUrl, null,
            new() { currency = "CNY", total = price }, new(openId), null, null, null);

        // request interface
        var basePayApis2 = new Senparc.Weixin.TenPayV3.TenPayHttpClient.BasePayApis2(_httpClient,
            _tenpayV3Setting);
        var result = await basePayApis2.JsApiAsync(jsApiRequestData);

        if (result.VerifySignSuccess ! = true)
        {
            throw new WeixinException("Error getting prepay_id result verification!") ;
        }

        // Get the UI information package
        var jsApiUiPackage = TenPaySignHelper.GetJsApiUiPackage(TenPayV3Info.AppId, result.prepay_id);
        ViewData["jsApiUiPackage"] = jsApiUiPackage;

        // Other logic
        //...

        return View();
    }
    catch (Exception ex)
    {
        //...
    }
}
```

> Reference file for this project:
>
> /**_Controllers/TenPayApiV3Controller.cs_**

In the above code, the `productId` parameter is the number of the product, which is used as a Sample to simulate the list and query from the memory, in the actual project, the product information is usually stored in the database, and the product data will be found according to the `productId`; the `hc` function is a HashCode of the corresponding product information, which is set in order to make sure the validity of the current memory information. The `hc` function is set to ensure the validity of the current memory information corresponding to the product information HashCode, the actual development projects do not need to use, can be ignored.

In this process, the most critical code is: `var result = TenPayOldV3.Unifiedorder(xmlDataInfo)`, `result.prepay_id` is the "prepayment ID", the front-end page must rely on prepay_id in order to make the mobile phone call the ID. id in order for the mobile phone to evoke WeChat payment. At this point, the current order number has already been registered in the WeChat Payment backend.

Note: This method uses the **[CustomOAuth]** feature, which is used to automatically use the OAuth function of WeChat public number to identify the user's identity, and this feature belongs to the public number category, so it will not be expanded here.

**Front-end**

The key operation of the front-end is to execute the JS code when the user clicks the "Pay" button:

```cs
// The WeixinJSBridgeReady event is triggered when the built-in WeChat browser completes its internal initialisation.
document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
    // Public Payment
    jQuery('a#getBrandWCPayRequest').click(function (e) {
        WeixinJSBridge.invoke('getBrandWCPayRequest', {
            "appId":"@jsApiUiPackage.AppId", // public name, passed in by the merchant
            "timeStamp":"@jsApiUiPackage.Timestamp", // timestamp
            "nonceStr":"@jsApiUiPackage.NonceStr", //random string
            "package":"@Html.Raw(jsApiUiPackage.PrepayIdPackage)", //extension package
            "signType": "RSA", //WeChat V3 Signature Type:RSA
            "paySign": "@Html.Raw(jsApiUiPackage.Signature)" //WeChat Signature
        }, function (res) {

            //alert(JSON.stringify(res));

            if (res.err_msg == "get_brand_wcpay_request:ok") {
                if (confirm('Payment successful! Click "OK" to enter the refund process test.')) {
                    location.href = '/Docs/TenPayV3/TenpayApiV3/Refund';
                }
                //console.log(JSON.stringify(res));
            }else{
                alert(JSON.stringify(res)); }
            }
            // Using the above method to determine the front-end return, the WeChat team would like to point out that res.err_msg will return ok if the user pays successfully, but it is not guaranteed to be reliable.
            // Therefore, the WeChat team suggests that when the ok return is received, the merchant back-end should be asked if it has received a notification that the transaction has been successful; if it has received a notification, the front-end should display the successful transaction interface; if it has not received a notification, the merchant back-end should take the initiative to call the Query Orders interface to query the current state of the order, and then feed back to the front-end to display the corresponding interface.
        }).

    });
```

> Note: WeChat Pay ApiV2 and ApiV3 have totally different difference in order interface, please pay attention if you upgrade!

> Reference file for this project:
>
> /**_Views/TenpayApiV3/JsApi.cshtml_**

**effect**

![订单支付页面](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-03.jpg)

![点击唤起支付](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-04.jpg)

![支付成功](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-dev-jsapi-05.jpg)
