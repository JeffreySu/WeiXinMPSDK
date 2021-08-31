﻿using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Senparc.Weixin.TenPayV3.TenPayApiRequest;

namespace Senparc.Weixin.TenPayV3
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
                    //以下代码参考：https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml
                    {"200" ,new[] { new TenPayApiResultCode("200","SUCCESS","请求成功","",true) } },
                    {"202" ,new[] { new TenPayApiResultCode("202", "USERPAYING", "用户支付中，需要输入密码", "等待5秒，然后调用被扫订单结果查询API，查询当前订单的不同状态，决定下一步的操作", false) } },
                    {"204" ,new[] { new TenPayApiResultCode("204", "No Content", "请求成功","", true) } },
                    {"400", new[] { new TenPayApiResultCode("400","PARAM_ERROR","参数错误","请根据接口返回的详细信息检查请求参数"),
                                    new TenPayApiResultCode("400","ORDER_CLOSED","订单已关闭","当前订单已关闭，请重新下单"),
                                    new TenPayApiResultCode("400","MCH_NOT_EXISTS","商户号不存在","请检查商户号是否正确"),
                                    new TenPayApiResultCode("400","INVALID_REQUEST","无效请求","请根据接口返回的详细信息检查"),
                                    new TenPayApiResultCode("400","APPID_MCHID_NOT_MATCH","appid和mch_id不匹配","请确认appid和mch_id是否匹配")
                                    }},
                    {"401", new[] { new TenPayApiResultCode("401","SIGN_ERROR","签名错误","请检查签名参数和方法是否都符合签名算法要求"),
                                    new TenPayApiResultCode("401","SIGN_ERROR","商户未设置加密的密钥","请登录商户平台操作！请参考http://kf.qq.com/faq/180830E36vyQ180830AZFZvu.html")//通过测试得到
                                    } },
                    {"403", new[] { new TenPayApiResultCode("403","TRADE_ERROR","交易错误","因业务原因交易失败，请查看接口返回的详细信息"),
                                    new TenPayApiResultCode("403","RULE_LIMIT","业务规则限制","因业务规则限制请求频率，请查看接口返回的详细信息"),
                                    new TenPayApiResultCode("403","OUT_TRADE_NO_USED","商户订单号重复","请核实商户订单号是否重复提交"),
                                    new TenPayApiResultCode("403","NOT_ENOUGH","余额不足","用户账号余额不足，请用户充值或更换支付卡后再支付"),
                                    new TenPayApiResultCode("403","NO_AUTH","商户无权限","请商户前往申请此接口相关权限"),
                                    new TenPayApiResultCode("403","ACCOUNT_ERROR","账号异常","用户账号异常，无需更多操作")
                                    }},
                    {"404", new[] { new TenPayApiResultCode("404","ORDER_NO_TEXIST","订单不存在","请检查订单是否发起过交易")} },
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
        /// <returns></returns>
        public static TenPayApiResultCode TryGetCode(HttpStatusCode httpStatusCode, string responseContent)
        {
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
                resultCode = new TenPayApiResultCode(httpStatusCode.ToString(), "UNKNOW CODE", "未知的代码", "请检查日志", false);//没有匹配到

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

        public bool Success { get; protected set; } = false;
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
