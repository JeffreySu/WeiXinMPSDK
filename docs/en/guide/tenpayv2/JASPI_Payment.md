# JASPI Payment

## JSAPI Payment

To use WeChat payment in WeChat public web page, you must complete the payment process with the help of JSAPI.

First of all, you need to create a product page and provide an entry point to place an order on the page, e.g. one-click purchase.
(implementation omitted)

In the current example, 3 key pages are provided: ProductList, ProductItem and JsApi (JSAPI order payment).

## ProductList ProductList

**Backend**

Reference code: ProductList() method under TenPayV3Controller.

```cs
public ActionResult ProductList()
{
    var products = ProductModel.GetFakeProductList();
    return View(products);
}
```

> Reference file for this project:
>
> /Controllers/**_TenPayV3Controller.cs_**

**Frontend**

> Reference file for this project:
>
> /Views/TenPayV3/**_ProductItem.cshtml_**

**Effects**

[Open Preview](https://sdk.weixin.senparc.com/TenPayV3/ProductList)

![商品列表](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-jsapi-01.png)

## ProductItem Product details

**Backend**

Reference code: ProductItem() method under TenPayV3Controller.

```cs
public ActionResult ProductItem(int productId, int hc)
{
    var products = ProductModel.GetFakeProductList();
    var product = products.FirstOrDefault(z => z.Id == productId);
    if (product == null || product.GetHashCode() ! = GetHashCode() !=hc)
    {
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
> /Controllers/**_TenPayV3Controller.cs_**

**Front-end**.

> Reference file for this project:
>
> /Views/TenPayV3/**_ProductItem.cshtml_**

**Effects**

> This project reference file: /Views/TenPayV3/ProductItem.cshtml

Select a product from [Product List](https://sdk.weixin.senparc.com/TenPayV3/ProductList) and click on it, you can see the detail page if you are on PC:

![商品列表](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-jsapi-02.png)

The above **Payment Method 2: "Sweep" Payment** There is a QR code automatically generated according to the selected product at the bottom of the page, use WeChat on your mobile phone to sweep it, then you can enter the corresponding product order page (i.e., JsApi order page).

## JSAPI order payment page

**Backend**

After the user clicks the order button, a prepaid order needs to be generated in the backend and registered on the page, please refer to `TenPayV3Controller.JsApi()` for the code.

```cs
[CustomOAuth(null, "/TenpayV3/OAuthCallback")]
public ActionResult JsApi(int productId, int hc)
{
  try

    {
        //Get the product information
        //...
        // Call JsApi to get prepayment information.
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
> /Controllers/TenPayV3Controller.cs

In the above code, the `productId` parameter is the number of the product, here as a Sample demonstration, it is a simulated list from memory and query, in the actual project, the product information is usually stored in the database, according to the `productId` from the data to find the product data; the `hc` function is to ensure the validity of the current memory information is set to the corresponding product information HashCode, you do not need to use it in the actual development project can be ignored. The `hc` function is set to ensure the validity of the current memory information corresponding to the product information HashCode, the actual development of the project need not be used, can be ignored.

In this process, the most critical code is: `var result = TenPayOldV3.Unifiedorder(xmlDataInfo)`, `result.prepay_id` is the "prepayment ID", the front-end page must rely on prepay_id in order to make the mobile phone call the ID. id in order for the mobile phone to evoke WeChat payment. At this point, the current order number has already been registered in the WeChat Payment backend.

Note: This method uses the **[CustomOAuth]** feature, which is used to automatically use the OAuth function of WeChat public number to identify the user's identity, and this feature belongs to the public number category, so it will not be expanded here.

**Front-end**

The key operation of the front-end is to execute the JS code when the user clicks the "Pay" button:

```JS
WeixinJSBridge.invoke(
  "getBrandWCPayRequest",
  {
    appId: "", //public name, passed in by merchant
    timeStamp: "", //timestamp
    nonceStr: "", //random string
    package: "", //extension package
    signType: "MD5", //WeChat signature type:MD5
    paySign: "", //WeChat signature
  },
  function (res) {
    if (res.err_msg == "get_brand_wcpay_request:ok") {
      //Payment successful
    }
  }
);
```

> Reference file for this project:
>
> /Views/TenPayV3/**_JsApi.cshtml_**

**Effects**

![订单支付页面](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-jsapi-03.jpg)

![点击唤起支付](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-jsapi-04.jpg)

![支付成功](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-dev-jsapi-05.jpg)
