# 电商收付通（Ecommerce）API 模块

本模块实现了微信支付V3电商收付通相关的API接口，为电商平台提供完整的支付解决方案。

## 功能概览

### 二级商户管理API
- **二级商户进件**：电商平台为二级商户发起进件申请
- **查询进件申请状态**：通过申请单号查询二级商户进件申请状态
- **通过业务申请编号查询申请状态**：通过业务申请编号查询二级商户进件申请状态

### 合单支付API
- **合单下单**：电商收付通合单支付，一次可以提交多个子商户的支付请求
- **合单查询订单**：查询合单支付订单状态
- **合单关闭订单**：关闭合单支付订单

### 分账API
- **请求分账**：电商收付通分账，将款项分账给指定的接收方
- **查询分账结果**：查询电商收付通分账结果
- **请求分账回退**：将已分账的款项回退给电商平台
- **查询分账回退结果**：查询分账回退结果
- **完结分账**：完结分账，不再进行后续分账操作

### 补差API
- **请求补差**：电商收付通补差，用于平台垫付资金的补差
- **请求补差回退**：将补差款项回退
- **取消补差**：取消补差

## API路径
所有电商收付通相关API的基础路径为：`v3/ecommerce`

## 文件结构
- `EcommerceApis.cs`：主要API类，包含所有电商收付通相关接口
- `Entities/RequestData/`：请求数据类
  - `EcommerceSubMerchantRequestData.cs`：二级商户管理请求数据
  - `EcommerceCombineRequestData.cs`：合单支付请求数据
  - `EcommerceProfitsharingRequestData.cs`：分账相关请求数据（待补充）
  - `EcommerceSubsidiesRequestData.cs`：补差相关请求数据（待补充）
- `Entities/ReturnJson/`：响应数据类
  - `EcommerceReturnJson.cs`：所有API响应数据
- `Entities/NotifyJson/`：回调通知数据类（待补充）

## 使用示例

```csharp
// 二级商户进件
var applymentData = new SubMerchantApplymentRequestData
{
    out_request_no = "APPLYMENT_00000000001",
    organization_type = "SUBJECT_TYPE_ENTERPRISE",
    business_license_info = new BusinessLicenseInfo
    {
        license_copy = "MediaID",
        license_number = "123456789012345678",
        merchant_name = "腾讯科技有限公司",
        legal_person = "张三"
    },
    // ... 其他字段
};
var applymentResult = await ecommerceApis.SubMerchantApplymentAsync(applymentData);

// 合单下单
var combineData = new CombineTransactionsRequestData
{
    combine_appid = "wxd678efh567hg6787",
    combine_mchid = "1900000109",
    combine_out_trade_no = "P20150806125346",
    sub_orders = new CombineSubOrder[]
    {
        new CombineSubOrder
        {
            mchid = "1900000110",
            out_trade_no = "20150806125347",
            description = "腾讯充值中心-QQ会员充值",
            amount = 100
        }
    },
    // ... 其他字段
};
var combineResult = await ecommerceApis.CombineTransactionsAsync(combineData);

// 请求分账
var profitsharingData = new EcommerceProfitsharingRequestData
{
    transaction_id = "1217752501201407033233368018",
    out_order_no = "P20150806125346",
    receivers = new ProfitsharingReceiver[]
    {
        new ProfitsharingReceiver
        {
            type = "MERCHANT_ID",
            account = "1900000109",
            amount = 100,
            description = "分给商户1900000109"
        }
    }
};
var profitsharingResult = await ecommerceApis.EcommerceProfitsharingAsync(profitsharingData);
```

## 注意事项
1. 电商收付通适用于电商平台场景，需要电商平台资质
2. 二级商户进件需要提供完整的商户资料，包括营业执照、身份证等
3. 合单支付最多支持50个子订单
4. 分账金额不能超过订单金额
5. 敏感信息需要按照微信支付要求进行加密传输
6. 部分API的请求数据类和分账、补差相关的数据类待进一步补充完善

## 应用场景
- **电商平台**：为平台上的商户提供支付服务
- **服务商模式**：为多个商户提供统一的支付解决方案
- **资金分账**：自动将订单资金分配给不同的参与方
- **平台补贴**：平台为用户提供补贴或优惠
