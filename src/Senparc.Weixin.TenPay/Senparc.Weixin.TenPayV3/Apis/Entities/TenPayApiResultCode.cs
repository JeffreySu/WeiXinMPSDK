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
  
    文件名：TenPayApiResultCode.cs
    文件功能描述：微信支付 API 请求返回 HTTP 状态代码
    
    
    创建标识：Senparc - 20210815

    修改标识：Senparc - 20220111
    修改描述：v0.6.8.7 优化 TenPayApiResultCode 获取逻辑，修复 TryGetCode() 方法中当匹配不到预设错误信息时，返回 null 的问题
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Senparc.Weixin.TenPayV3.TenPayApiRequest;

namespace Senparc.Weixin.TenPayV3.Apis.Entities
{
    /// <summary>
    /// 微信支付 API 请求返回 HTTP 状态代码
    /// </summary>
    public record class TenPayApiResultCode
    {
        /// <summary>
        /// 已知返回代码集合
        /// </summary>
        public static Dictionary<string, TenPayApiResultCode[]> CodesCollection = new Dictionary<string, TenPayApiResultCode[]> {
                    //以下代码通过测试得到：
                    {"405",new[]{ new TenPayApiResultCode("405", "MethodNotAllowed","为允许请求方法","请检查是否符合文档要求的 GET / POST 等方法") } },
                    //以下代码参考：
                    //https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml
                    //https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_1.shtml
                    {"200" ,new[] { new TenPayApiResultCode("200","SUCCESS","请求成功","",true) } },
                    {"202" ,new[] { new TenPayApiResultCode("202", "USERPAYING", "用户支付中，需要输入密码", "等待5秒，然后调用被扫订单结果查询API，查询当前订单的不同状态，决定下一步的操作", false) } },
                    {"204" ,new[] { new TenPayApiResultCode("204", "No Content", "请求成功","", true) } },
                    {"400", new[] { new TenPayApiResultCode("400","PARAM_ERROR","参数错误","请根据接口返回的详细信息检查请求参数"),
                                    new TenPayApiResultCode("400","ORDER_CLOSED","订单已关闭","当前订单已关闭，请重新下单"),
                                    new TenPayApiResultCode("400","MCH_NOT_EXISTS","商户号不存在","请检查商户号是否正确"),
                                    new TenPayApiResultCode("400","INVALID_REQUEST","无效请求","请根据接口返回的详细信息检查"),
                                    new TenPayApiResultCode("400","APPID_MCHID_NOT_MATCH","appid和mch_id不匹配","请确认appid和mch_id是否匹配"),

                                    new TenPayApiResultCode("400","PARAM_ERROR","回调url不能为空","请填写回调url",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","回调商户不能为空","请填写回调商户",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","券id必填","请填写券id",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","appid必填","请输入appid",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","openid必填","请输入openid",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","页大小超过阈值","请不要超过最大的页大小",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","输入时间格式错误","请输入正确的时间格式",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","批次号必填","请输入批次号",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","商户号必填","请输入商户号",false),
                                    new TenPayApiResultCode("400","PARAM_ERROR","非法的批次状态","请检查批次状态",false),
                                    new TenPayApiResultCode("400","MCH_NOT_EXISTS","商户号不合法","请输入正确的商户号",false),
                                    new TenPayApiResultCode("400","INVALID_REQUEST","openid与appid不匹配","请使用appid下的openid",false),
                                    new TenPayApiResultCode("400","INVALID_REQUEST","活动已结束或未激活","请检查批次状态",false),
                                    new TenPayApiResultCode("400","INVALID_REQUEST","非法的商户号","请检查商户号是否正确",false),
                                    new TenPayApiResultCode("400","APPID_MCHID_NOT_MATCH","商户号与appid不匹配","请绑定调用接口的商户号和appid后重试",false),
                                    new TenPayApiResultCode("400","RESOURCE_ALREADY_EXISTS","创建请求重入，但本次请求与上次请求信息不一致","创建请求重入，但本次请求与上次请求信息不一致",false)                                    }},
                    {"401", new[] { new TenPayApiResultCode("401","SIGN_ERROR","签名错误","请检查签名参数和方法是否都符合签名算法要求"),
                                    new TenPayApiResultCode("401","SIGN_ERROR","商户未设置加密的密钥","请登录商户平台操作！请参考http://kf.qq.com/faq/180830E36vyQ180830AZFZvu.html")//通过测试得到
                                    }},
                    {"403", new[] { new TenPayApiResultCode("403","TRADE_ERROR","交易错误","因业务原因交易失败，请查看接口返回的详细信息"),
                                    new TenPayApiResultCode("403","RULE_LIMIT","业务规则限制","因业务规则限制请求频率，请查看接口返回的详细信息"),
                                    new TenPayApiResultCode("403","OUT_TRADE_NO_USED","商户订单号重复","请核实商户订单号是否重复提交"),
                                    new TenPayApiResultCode("403","NOT_ENOUGH","余额不足","用户账号余额不足，请用户充值或更换支付卡后再支付"),
                                    new TenPayApiResultCode("403","NO_AUTH","商户无权限","请商户前往申请此接口相关权限"),
                                    new TenPayApiResultCode("403","ACCOUNT_ERROR","账号异常","用户账号异常，无需更多操作"),

                                    new TenPayApiResultCode("403","USER_ACCOUNT_ABNORMAL","用户非法","该用户账号异常，无法领券。商家可联系微信支付或让用户联系微信支付客服处理。",false),
                                    new TenPayApiResultCode("403","NOT_ENOUGH","批次预算不足","请补充预算",false),
                                    new TenPayApiResultCode("403","REQUEST_BLOCKED","调用商户无权限","请开通产品权限后再调用该接口",false),
                                    new TenPayApiResultCode("403","REQUEST_BLOCKED","商户无权发券","调用接口的商户号无权发券，请检查是否是自己的批次或是已授权的批次。",false),
                                    new TenPayApiResultCode("403","REQUEST_BLOCKED","批次不支持跨商户发券","该批次未做跨商户号的授权，请授权后再发放",false),
                                    new TenPayApiResultCode("403","REQUEST_BLOCKED","用户被限领拦截","用户领取已经达到上限，请调高上限或停止发放。",false),
                                    new TenPayApiResultCode("403","REQUEST_BLOCKED","活动未开始或已结束","该活动未开始或已结束"),
                                    new TenPayApiResultCode("403","NO_AUTH","你配置的信息需要开通特殊权限","请参考：QA方案",false)
                                    }},
                    {"404", new[] { new TenPayApiResultCode("404","ORDER_NO_TEXIST","订单不存在","请检查订单是否发起过交易"),

                                    new TenPayApiResultCode("404","RESOURCE_NOT_EXISTS","批次不存在","请检查批次ID是否正确",false),
                                    }},
                    {"429", new[] { new TenPayApiResultCode("429","FREQUENCY_LIMITED","频率超限","请降低请求接口频率")} },
                    {"500", new[] { new TenPayApiResultCode("500","SYSTEM_ERROR","系统错误","系统异常，请用相同参数重新调用"),
                                    new TenPayApiResultCode("500","INVALID_TRANSACTIONID","订单号非法","请检查微信支付订单号是否正确"),
                                    new TenPayApiResultCode("500","BANK_ERROR","银行系统异常","银行系统异常，请用相同参数重新调用"),
                                    new TenPayApiResultCode("500","OPENID_MISMATCH","openid和appid不匹配","请确认openid和appid是否匹配")
                                    }}
                    };

        /// <summary>
        /// 尝试获取 HTTP 返回代码，并附带备注信息
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="responseContent"></param>
        /// <returns></returns>
        public static TenPayApiResultCode TryGetCode(HttpStatusCode httpStatusCode, string responseContent)
        {
            //responseContent ??= "{}";
                
            TenPayApiResultCode resultCode = null;

            if (CodesCollection.TryGetValue(((int)httpStatusCode).ToString(), out var result))
            {
                //只有一条符合的结果
                if (result.Length == 1)
                {
                    return result[0] with { };
                }

                //有多条符合的结果
                var firstResult = result.First();
                if (firstResult.Success)
                {
                    resultCode = firstResult;//请求成功
                }
                else
                {
                    //失败，如：{"code":"PARAM_ERROR","detail":{"location":null,"value":["/body/payer/openid"]},"message":"请求中含有未在API文档中定义的参数"}
                    var responseErrorMessage = responseContent.GetObject<ResponseErrorJsonResult>();
                    if (responseErrorMessage != null)
                    {
                        //content符合标准的错误格式
                        resultCode = result.FirstOrDefault(z => z.ErrorCode == responseErrorMessage.code);//查找符合标准的对象
                        if (resultCode != null)
                        {
                            resultCode = resultCode with { Additional = $"发生错误：{responseErrorMessage.ToJson()}" };
                        }
                        else
                        {
                            resultCode = new TenPayApiResultCode(((int)httpStatusCode).ToString(), responseErrorMessage.code, "未找到匹配的错误信息", $"发生错误：{responseErrorMessage.ToJson()}");
                        }
                    }
                    else
                    {
                        //content 不符合标准的错误格式，返回所有错误结果
                        resultCode = firstResult with
                        {
                            ErrorCode = string.Join(";\n", result.Select(z => z.ErrorCode)),
                            ErrorMessage = string.Join(";\n", result.Select(z => z.ErrorMessage)),
                            Solution = string.Join(";\n", result.Select(z => z.Solution)),
                        };
                    }
                }
            }
            else
            {
                //错误代码不在预设范围内
                resultCode = new TenPayApiResultCode(((int)httpStatusCode).ToString(), "UNKNOW CODE", "未知的代码", "请检查日志", false);//没有匹配到

                var responseErrorMessage = responseContent.GetObject<ResponseErrorJsonResult>();
                if (responseErrorMessage != null)
                {
                    resultCode.ErrorCode = responseErrorMessage.code;
                    resultCode.ErrorMessage = responseErrorMessage.message;
                    resultCode.Additional = $"发生未知代码错误，请检查日志：{responseErrorMessage.ToJson()}";
                }
            }

            return resultCode;
        }

        /// <summary>
        /// 是否为成功消息
        /// </summary>
        public bool Success { get; protected set; } = false;
        /// <summary>
        /// HttpStatusCode
        /// </summary>
        public string StateCode { get; protected set; }
        public string ErrorCode { get; protected set; }
        public string ErrorMessage { get; internal set; }
        public string Solution { get; internal set; }
        /// <summary>
        /// 额外信息
        /// </summary>
        public string Additional { get; set; }

        public TenPayApiResultCode() { }

        public TenPayApiResultCode(string stateCode, string errorCode, string errorMessage, string solution, bool success = false)
        {
            this.StateCode = stateCode;
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.Solution = solution;
            this.Success = success;
        }

    }
}
