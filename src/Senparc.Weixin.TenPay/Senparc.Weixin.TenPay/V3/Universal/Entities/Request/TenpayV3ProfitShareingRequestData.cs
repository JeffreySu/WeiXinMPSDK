#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
  
    文件名：TenpayV3ProtfitSharingRequestData.cs
    文件功能描述：分账请求
    
    创建标识：hesi815 - 20200318

    修改标识：15989221023 - 20200416
    修改描述：v1.5.402 添加 Version 参数 https://github.com/JeffreySu/WeiXinMPSDK/pull/2151

    修改标识：15989221023 - 20200511
    修改描述：v1.5.502.1 添加 TenPayV3UnifiedorderRequestData Version 参数 https://github.com/JeffreySu/WeiXinMPSDK/pull/2174
   
    修改标识：Senparc - 20200528
    修改描述：v1.5.502.2 fix bug:必须指定待分账的接收方列表 判断有误 https://github.com/JeffreySu/WeiXinMPSDK/issues/2181

    修改标识：Senparc - 20200601
    修改描述：v1.5.502.3 fix bug:必须指定待分账的接收方列表 判断有误 https://github.com/JeffreySu/WeiXinMPSDK/issues/2184

    修改标识：Senparc - 20201209
    修改描述：v1.6.100 更新 TenPayV3UnifiedorderRequestData 构造函数，version 为空时忽略 https://github.com/JeffreySu/WeiXinMPSDK/issues/2277

    修改标识：Senparc - 20210202
    修改描述：v1.6.200.2 修复：调用分账查询接口, 结果返回"验证签名失败"问题 https://github.com/JeffreySu/WeiXinMPSDK/issues/2309

----------------------------------------------------------------*/

using Newtonsoft.Json;
using Senparc.CO2NET.Extensions;
using System;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 分账请求
    /// <para>服务商(单次分账): <see href="https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_1&amp;index=1"></see></para>
    /// <para>境内普通商户(单次分账): <see href="https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_1&amp;index=1"></see></para>
    /// <para>服务商(多次分账): <see href="https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_6&amp;index=2"></see></para>
    /// <para>境内普通商户(多次分账): <see href="https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_6&amp;index=2"></see></para>
    /// <para>2019-12-30</para>
    /// </summary>
    public class TenpayV3ProtfitSharingRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string MchId { get; set; }


        #region 服务商

        /// <summary>
        /// 服务商时的子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 服务商时的子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        #region 订单信息
        /// <summary>
        /// 微信支付订单号 
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统内部的分账单号，在商户系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），
        /// 同一分账单号多次请求等同一次。
        /// 只能是数字、大小写字母_-|*@  
        /// </summary>
        public string OutOrderNo { get; set; }
        #endregion

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }
        /// <summary>
        /// 签名类型， HMAC-SHA256;
        /// 签名类型，目前只支持HMAC-SHA256
        /// </summary>
        public readonly string SignType = "HMAC-SHA256";

        /// <summary>
        /// 分账接收方对象，json格式
        /// </summary>
        public TenpayV3ProfitShareingRequestData_ReceiverInfo[] Receivers { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 此不带参数的构造函数是为了反序列化的实例初始化，提交数据时请使用其他构造函数
        /// </summary>
        public TenpayV3ProtfitSharingRequestData()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="subappid">服务商时,子商户公众账号ID</param>
        /// <param name="submchid">服务商时,子商户号</param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="transactionId">微信支付订单号</param>
        /// <param name="outOrderNo">商户系统内部的分账单号，在商户系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），
        /// 同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@ 
        /// </param>
        /// <param name="receivers">分账接收方列表,此对象通过Json格式传输</param>
        public TenpayV3ProtfitSharingRequestData(
            string appId, string mchId, string subappid, string submchid, string key, string nonceStr,
            string transactionId,
            string outOrderNo,
            TenpayV3ProfitShareingRequestData_ReceiverInfo[] receivers
        )
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;
            SubAppId = subappid;
            SubMchId = submchid;
            Receivers = receivers;

            if (Receivers == null || Receivers.Length == 0)
            {
                throw new ArgumentNullException("必须指定待分账的接收方列表");
            }
            TransactionId = transactionId;
            OutOrderNo = outOrderNo;


            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
            PackageRequestHandler.SetParameter("appid", this.AppId);                       //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                      //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId);     //子商户公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId);    //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType);     //签名类型，默认为MD5

            PackageRequestHandler.SetParameter("transaction_id", this.TransactionId);                //微信支付订单号
            PackageRequestHandler.SetParameter("out_order_no", this.OutOrderNo);     //商户系统内部的分账单号
            if (Receivers != null)
            {
                JsonSerializerSettings setting = new JsonSerializerSettings();
                setting.NullValueHandling = NullValueHandling.Ignore;
                PackageRequestHandler.SetParameter("receivers", Receivers.ToJson(false, setting));   //分账接收方列表
            }

            Sign = PackageRequestHandler.CreateSha256Sign("key", this.Key);

            PackageRequestHandler.SetParameter("sign", Sign);                              //签名
            #endregion
        }
    }


    /// <summary>
    /// 完结分账请求
    /// <para>服务商特约商户:<see href="https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_5&amp;index=6"></see></para>
    /// <para>境内普通商户: <see href="https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_5&amp;index=6"></see></para>
    /// <para>2019-12-30</para>
    /// </summary>
    public class TenpayV3ProtfitSharingFinishRequestData
    {

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string MchId { get; set; }


        #region 服务商

        /// <summary>
        /// 服务商时的子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 服务商时的子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        #region 订单信息
        /// <summary>
        /// 微信支付订单号 
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统内部的分账单号，在商户系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），
        /// 同一分账单号多次请求等同一次。
        /// 只能是数字、大小写字母_-|*@  
        /// </summary>
        public string OutOrderNo { get; set; }

        /// <summary>
        /// 分账完结的原因描述,
        /// </summary>
        public string Description { get; set; }
        #endregion

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }
        /// <summary>
        /// 签名类型， HMAC-SHA256;
        /// 签名类型，目前只支持HMAC-SHA256
        /// </summary>
        public readonly string SignType = "HMAC-SHA256";


        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 此不带参数的构造函数是为了反序列化的实例初始化，提交数据时请使用其他构造函数
        /// </summary>
        public TenpayV3ProtfitSharingFinishRequestData()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="subappid">服务商时,子商户公众账号ID</param>
        /// <param name="submchid">服务商时,子商户号</param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="transactionId">微信支付订单号</param>
        /// <param name="outOrderNo">商户系统内部的分账单号，在商户系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），
        /// 同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@ 
        /// </param>
        /// <param name="description">分账完结的原因描述 </param>
        public TenpayV3ProtfitSharingFinishRequestData(
            string appId, string mchId, string subappid, string submchid, string key, string nonceStr,
            string transactionId,
            string outOrderNo,
            string description
        )
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;
            SubAppId = subappid;
            SubMchId = submchid;
            Description = description;

            TransactionId = transactionId;
            OutOrderNo = outOrderNo;


            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
            PackageRequestHandler.SetParameter("appid", this.AppId);                       //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                      //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId);     //子商户公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId);    //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType);     //签名类型

            PackageRequestHandler.SetParameter("transaction_id", this.TransactionId);                //微信支付订单号
            PackageRequestHandler.SetParameter("out_order_no", this.OutOrderNo);     //商户系统内部的分账单号
            PackageRequestHandler.SetParameter("description", this.Description);     //商户系统内部的分账单号


            Sign = PackageRequestHandler.CreateSha256Sign("key", this.Key);

            PackageRequestHandler.SetParameter("sign", Sign);                              //签名
            #endregion
        }
    }



    /// <summary>
    /// 分账查询请求参数
    /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_2&index=3
    /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_2&index=3
    /// 2019-12-30
    /// </summary>
    public class TenpayV3ProtfitSharingQueryRequestData
    {

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string MchId { get; set; }


        #region 服务商

        /// <summary>
        /// 服务商时的子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 服务商时的子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        #region 订单信息
        /// <summary>
        /// 微信支付订单号 
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 商户系统内部的分账单号，在商户系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），
        /// 同一分账单号多次请求等同一次。
        /// 只能是数字、大小写字母_-|*@  
        /// </summary>
        public string OutOrderNo { get; set; }
        #endregion

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }
        /// <summary>
        /// 签名类型， HMAC-SHA256;
        /// 签名类型，目前只支持HMAC-SHA256
        /// </summary>
        public readonly string SignType = "HMAC-SHA256";


        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 此不带参数的构造函数是为了反序列化的实例初始化，提交数据时请使用其他构造函数
        /// </summary>
        public TenpayV3ProtfitSharingQueryRequestData()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId">按照 https://github.com/JeffreySu/WeiXinMPSDK/issues/2309 的反馈，可以输入 null</param>
        /// <param name="mchId"></param>
        /// <param name="subappid">服务商时,子商户公众账号ID</param>
        /// <param name="submchid">服务商时,子商户号</param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="transactionId">微信支付订单号</param>
        /// <param name="outOrderNo">商户系统内部的分账单号，在商户系统内部唯一（单次分账、多次分账、完结分账应使用不同的商户分账单号），
        /// 同一分账单号多次请求等同一次。只能是数字、大小写字母_-|*@ 
        /// </param>
        /// <param name="description"></param>
        public TenpayV3ProtfitSharingQueryRequestData(
            string appId, string mchId, string subappid, string submchid, string key, string nonceStr,
            string transactionId,
            string outOrderNo,
            string description
        )
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;
            SubAppId = subappid;
            SubMchId = submchid;

            TransactionId = transactionId;
            OutOrderNo = outOrderNo;


            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
            PackageRequestHandler.SetParameterWhenNotNull("appid", this.AppId);                       //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                      //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId);     //子商户公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId);    //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType);     //签名类型

            PackageRequestHandler.SetParameter("transaction_id", this.TransactionId);                //微信支付订单号
            PackageRequestHandler.SetParameter("out_order_no", this.OutOrderNo);     //商户系统内部的分账单号


            Sign = PackageRequestHandler.CreateSha256Sign("key", this.Key);

            PackageRequestHandler.SetParameter("sign", Sign);                              //签名
            #endregion
        }
    }

    /// <summary>
    /// 添加分账接收方的请求参数
    /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_3&index=4
    /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_3&index=4
    /// 2019-12-30
    /// </summary>
    public class TenpayV3ProfitShareingAddReceiverRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string MchId { get; set; }


        #region 服务商

        /// <summary>
        /// 服务商时的子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 服务商时的子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }
        /// <summary>
        /// 签名类型， HMAC-SHA256;
        /// 签名类型，目前只支持HMAC-SHA256
        /// </summary>
        public readonly string SignType = "HMAC-SHA256";

        /// <summary>
        /// 分账接收方对象，json格式
        /// </summary>
        public TenpayV3ProfitShareingAddReceiverRequestData_ReceiverInfo Receiver { get; set; }


        /// <summary>
        /// 此不带参数的构造函数是为了反序列化的实例初始化，提交数据时请使用其他构造函数
        /// </summary>
        public TenpayV3ProfitShareingAddReceiverRequestData()
        {
        }

        /// <summary>
        /// 服务商
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="subappid">服务商时,子商户公众账号ID</param>
        /// <param name="submchid">服务商时,子商户号</param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="receiver">新添加的分账接收方对象
        /// </param>
        public TenpayV3ProfitShareingAddReceiverRequestData(
            string appId, string mchId, string subappid, string submchid, string key, string nonceStr,
            TenpayV3ProfitShareingAddReceiverRequestData_ReceiverInfo receiver
        )
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;
            SubAppId = subappid;
            SubMchId = submchid;
            Receiver = receiver;
            if (Receiver == null)
            {
                throw new ArgumentNullException("必须指定待添加的分账接收方");
            }

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
            PackageRequestHandler.SetParameter("appid", this.AppId);                       //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                      //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId);     //子商户公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId);    //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType);     //签名类型，默认为MD5

            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            PackageRequestHandler.SetParameter("receiver", Receiver.ToJson(false, setting));   //场景信息

            Sign = PackageRequestHandler.CreateSha256Sign("key", this.Key);

            PackageRequestHandler.SetParameter("sign", Sign);                              //签名
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;
    }

    /// <summary>
    /// 删除分账接收方的请求参数
    /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_4&index=5
    /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_4&index=5
    /// 2019-12-30
    /// </summary>
    public class TenpayV3ProfitShareingRemoveReceiverRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string MchId { get; set; }


        #region 服务商

        /// <summary>
        /// 服务商时的子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 服务商时的子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }
        /// <summary>
        /// 签名类型， HMAC-SHA256;
        /// 签名类型，目前只支持HMAC-SHA256
        /// </summary>
        public readonly string SignType = "HMAC-SHA256";

        /// <summary>
        /// 分账接收方对象，json格式
        /// </summary>
        public TenpayV3ProfitShareing_ReceiverInfo Receiver { get; set; }
        /// <summary>
        /// 统一下单接口参数，参考：https://pay.weixin.qq.com/wiki/doc/api/danpin.php?chapter=9_203&amp;index=6
        /// </summary>
        public string Version { get; set; }


        /// <summary>
        /// 此不带参数的构造函数是为了反序列化的实例初始化，提交数据时请使用其他构造函数
        /// </summary>
        public TenpayV3ProfitShareingRemoveReceiverRequestData()
        {
        }

        /// <summary>
        /// 服务商
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="subappid">子商户公众账号ID</param>
        /// <param name="submchid">子商户号</param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        /// <param name="receiver">需要删除的的分账接收方对象</param>
        /// <param name="version">统一下单接口参数，参考：https://pay.weixin.qq.com/wiki/doc/api/danpin.php?chapter=9_203&amp;index=6</param>
        public TenpayV3ProfitShareingRemoveReceiverRequestData(
            string appId, string mchId, string subappid, string submchid, string key, string nonceStr,
            TenpayV3ProfitShareing_ReceiverInfo receiver, string version = null
        )
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;
            SubAppId = subappid;
            SubMchId = submchid;
            Receiver = receiver;
            Version = version;
            if (Receiver == null)
            {
                throw new ArgumentNullException("必须指定待删除的分账接收方");
            }


            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1

            PackageRequestHandler.SetParameterWhenNotNull("version", Version);
            PackageRequestHandler.SetParameter("appid", this.AppId);                        //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                       //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId);      //子商户公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId);     //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                 //随机字符串
            PackageRequestHandler.SetParameter("sign_type", this.SignType);                 //签名类型，默认为MD5

            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            PackageRequestHandler.SetParameter("receiver", Receiver.ToJson(false, setting));//场景信息

            Sign = PackageRequestHandler.CreateSha256Sign("key", this.Key);

            PackageRequestHandler.SetParameter("sign", Sign);                               //签名
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;
    }
}
