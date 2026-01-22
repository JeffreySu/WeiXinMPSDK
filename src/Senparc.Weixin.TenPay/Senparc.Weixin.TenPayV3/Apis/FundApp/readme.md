## 【资金应用 - 商家转账】接口对应文档

[https://pay.weixin.qq.com/doc/v3/merchant/4012716434](https://pay.weixin.qq.com/doc/v3/merchant/4012716434) 下的【发起转账】所有接口

### 功能说明

商家转账功能用于实现商户向用户发起转账，适用于提现、奖励发放、报销等场景。

### 接口列表

#### 发起转账相关
- **发起转账API** - `TransferBillAsync`
  - 接口地址：POST `/v3/fund-app/mch-transfer/transfer-bills`
  - 说明：商家转账用户确认模式下，用户申请收款时，商户可通过此接口申请创建转账单

- **撤销转账API** - `CancelTransferAsync`
  - 接口地址：POST `/v3/fund-app/mch-transfer/transfer-bills/out-bill-no/{out_bill_no}/cancel`
  - 说明：商户通过转账接口发起付款后，在用户确认收款之前可以通过该接口撤销付款

- **商户单号查询转账单API** - `QueryTransferByOutBillNoAsync`
  - 接口地址：GET `/v3/fund-app/mch-transfer/transfer-bills/out-bill-no/{out_bill_no}`
  - 说明：商户可通过商户单号查询转账单据详情

- **微信单号查询转账单API** - `QueryTransferByBillNoAsync`
  - 接口地址：GET `/v3/fund-app/mch-transfer/transfer-bills/transfer-bill-no/{transfer_bill_no}`
  - 说明：商户可通过微信转账单号查询转账单据详情

#### 电子回单相关
- **商户单号申请电子回单API** - `ApplyElecsignByOutBillNoAsync`
  - 接口地址：POST `/v3/fund-app/mch-transfer/elecsign/out-bill-no`
  - 说明：商户可以指定商户转账单号通过该接口申请商家转账用户确认模式转账单据对应的电子回单

- **商户单号查询电子回单API** - `QueryElecsignByOutBillNoAsync`
  - 接口地址：GET `/v3/fund-app/mch-transfer/elecsign/out-bill-no/{out_bill_no}`
  - 说明：商户可以指定商户转账单号通过该接口查询电子回单申请和处理进度

- **微信单号申请电子回单API** - `ApplyElecsignByBillNoAsync`
  - 接口地址：POST `/v3/fund-app/mch-transfer/elecsign/transfer-bill-no`
  - 说明：商户可以指定微信转账单号通过该接口申请商家转账用户确认模式转账单据对应的电子回单

- **微信单号查询电子回单API** - `QueryElecsignByBillNoAsync`
  - 接口地址：GET `/v3/fund-app/mch-transfer/elecsign/transfer-bill-no/{transfer_bill_no}`
  - 说明：商户可以指定微信转账单号通过该接口查询电子回单申请和处理进度

### 使用示例

```csharp
// 初始化 FundApp API
var fundAppApis = new FundAppApis();

// 发起转账
var transferRequest = new TransferBillRequestData
{
    appid = "wxf636efh567hg4356",
    out_bill_no = "plfk2020042013",
    transfer_scene_id = "1000",
    openid = "o-MYE42l80oelYMDE34nYD456Xoy",
    transfer_amount = 400000, // 单位：分
    transfer_remark = "新会员开通有礼",
    notify_url = "https://www.weixin.qq.com/wxpay/pay.php",
    user_recv_perception = "现金奖励"
};

var result = await fundAppApis.TransferBillAsync(transferRequest);

// 查询转账单
var queryRequest = new QueryTransferByOutBillNoRequestData
{
    out_bill_no = "plfk2020042013"
};

var queryResult = await fundAppApis.QueryTransferByOutBillNoAsync(queryRequest);
```

### 注意事项

1. **转账场景ID**：需要在商户平台-产品中心-商家转账中申请对应的场景ID
2. **收款用户姓名**：转账金额 >= 2000元时必填，需使用微信提供的公钥进行加密
3. **金额单位**：转账金额单位为"分"，不是"元"
4. **通知地址**：notify_url必须为公网可访问的HTTPS地址
5. **单据状态**：
   - ACCEPTED: 转账已受理
   - PROCESSING: 转账锁定资金中
   - WAIT_USER_CONFIRM: 待收款用户确认
   - TRANSFERING: 转账中
   - SUCCESS: 转账成功
   - FAIL: 转账失败
   - CANCELING: 撤销中
   - CANCELLED: 转账撤销完成
