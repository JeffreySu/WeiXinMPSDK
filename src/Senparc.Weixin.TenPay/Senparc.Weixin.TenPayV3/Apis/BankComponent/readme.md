# 银行组件（BankComponent）API 模块

本模块实现了微信支付V3银行组件相关的API接口，提供银行信息查询功能。

## 功能概览

### 银行查询API
- **获取对私银行卡号开户银行**：通过银行卡号查询开户银行信息
- **查询支持个人业务的银行列表**：查询微信支付支持的银行列表
- **查询省份列表**：查询微信支付支持的省份列表
- **查询城市列表**：根据省份查询城市列表
- **查询支行列表**：根据银行和城市查询支行列表

## API路径
所有银行组件相关API的基础路径为：`v3/capital/capitallhh`

## 文件结构
- `BankComponentApis.cs`：主要API类，包含所有银行组件相关接口
- `Entities/RequestData/`：请求数据类
  - `BankComponentRequestData.cs`：银行组件API请求数据
- `Entities/ReturnJson/`：响应数据类
  - `BankComponentReturnJson.cs`：所有API响应数据

## 使用示例

```csharp
// 获取对私银行卡号开户银行
var queryBankData = new QueryBankRequestData
{
    account_number = "6225880137005***" // 需要加密处理
};
var bankResult = await bankComponentApis.QueryBankAsync(queryBankData);

// 查询支持个人业务的银行列表
var queryBankListData = new QueryBankListRequestData
{
    offset = 0,
    limit = 20
};
var bankListResult = await bankComponentApis.QueryBankListAsync(queryBankListData);

// 查询省份列表
var provinceListResult = await bankComponentApis.QueryProvinceListAsync();

// 查询城市列表
var queryCityData = new QueryCityListRequestData
{
    province_code = "110000"
};
var cityListResult = await bankComponentApis.QueryCityListAsync(queryCityData);

// 查询支行列表
var queryBranchData = new QueryBranchListRequestData
{
    bank_alias_code = "ICBC",
    city_code = "110100",
    offset = 0,
    limit = 20
};
var branchListResult = await bankComponentApis.QueryBranchListAsync(queryBranchData);
```

## 注意事项
1. 银行卡号等敏感信息需要按照微信支付要求进行加密传输
2. 分页查询时，limit参数最大值为50
3. 查询支行列表前需要先查询银行列表确认该银行是否需要填写支行信息
4. 省份编码和城市编码遵循国家标准行政区划代码
