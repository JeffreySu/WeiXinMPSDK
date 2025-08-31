# 电子发票（Fapiao）API 模块

本模块实现了微信支付V3电子发票相关的API接口。

## 功能概览

### 公共API
- **检查子商户开票功能状态**：检查子商户是否开通电子发票功能
- **创建电子发票卡券模板**：创建电子发票卡券模板，用于生成电子发票
- **查询电子发票**：查询已开具的电子发票信息
- **获取抬头填写链接**：获取用户填写发票抬头的链接
- **获取用户填写的抬头**：获取用户已填写的发票抬头信息

### 区块链电子发票API
- **获取商户开票基础信息**：获取商户在微信支付开票系统中的基础信息
- **开具电子发票**：向税务局请求开具电子发票
- **冲红电子发票**：冲红已开具的电子发票
- **获取发票下载信息**：获取发票文件的下载信息

## API路径
所有电子发票相关API的基础路径为：`v3/new-tax-control-fapiao`

## 文件结构
- `FapiaoApis.cs`：主要API类，包含所有电子发票相关接口
- `Entities/RequestData/`：请求数据类
  - `FapiaoPublicRequestData.cs`：公共API请求数据
  - `FapiaoBlockchainRequestData.cs`：区块链电子发票API请求数据
- `Entities/ReturnJson/`：响应数据类
  - `FapiaoReturnJson.cs`：所有API响应数据

## 使用示例

```csharp
// 检查子商户开票功能状态
var checkData = new CheckFapiaoStatusRequestData
{
    sub_mchid = "1900000109"
};
var checkResult = await fapiaoApis.CheckSubMerchantFapiaoStatusAsync(checkData);

// 开具电子发票
var createData = new CreateFapiaoRequestData
{
    mchid = "1900000109",
    sub_mchid = "1900000109",
    transaction_id = "1217752501201407033233368018",
    fapiao_apply_time = "2021-06-10",
    // ... 其他字段
};
var createResult = await fapiaoApis.CreateFapiaoAsync(createData);
```

## 注意事项
1. 电子发票功能需要商户开通相关权限
2. 敏感信息需要按照微信支付要求进行加密传输
3. 发票开具涉及税务系统，处理时间可能较长
4. 冲红操作需要谨慎处理，一旦执行将不可撤销
