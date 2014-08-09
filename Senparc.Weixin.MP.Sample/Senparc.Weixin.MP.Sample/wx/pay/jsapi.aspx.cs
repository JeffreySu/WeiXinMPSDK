using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using Senparc.Weixin.MP.WeixinPayLib;
//=================================
//JSAPI支付
//=================================
public partial class _Default : System.Web.UI.Page
{
    public String appId = WeixinPayUtil.AppId;
    public String timeStamp = "";
    public String nonceStr = "";
    public String packageValue = "";
    public String paySign = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sp_billno = Request["order_no"];
        //当前时间 yyyyMMdd
        string date = DateTime.Now.ToString("yyyyMMdd");

        if (null == sp_billno)
        {
            //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            sp_billno = DateTime.Now.ToString("HHmmss") + WeixinPayUtil.BuildRandomStr(4);
        }
        else
        {
            sp_billno = Request["order_no"].ToString();
        }

        sp_billno = WeixinPayUtil.PartnerId + sp_billno;



        //创建支付应答对象
        RequestHandler packageReqHandler = new RequestHandler(Context);
        //初始化
        packageReqHandler.Init();


        //设置package订单参数
        packageReqHandler.SetParameter("partner", WeixinPayUtil.PartnerId);		  //商户号
        packageReqHandler.SetParameter("fee_type", "1");                    //币种，1人民币
        packageReqHandler.SetParameter("input_charset", "GBK");
        packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
        packageReqHandler.SetParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
        packageReqHandler.SetParameter("notify_url", WeixinPayUtil.TenpayNotify);		    //接收财付通通知的URL
        packageReqHandler.SetParameter("body", "JSAPIdemo");	                    //商品描述
        packageReqHandler.SetParameter("spbill_create_ip", Page.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP

        //获取package包
        packageValue = packageReqHandler.GetRequestURL();

        //调起微信支付签名
        timeStamp = WeixinPayUtil.GetTimestamp();
        nonceStr = WeixinPayUtil.GetNoncestr();

        //设置支付参数
        RequestHandler paySignReqHandler = new RequestHandler(Context);
        paySignReqHandler.SetParameter("appid", appId);
        paySignReqHandler.SetParameter("appkey", WeixinPayUtil.AppKey);
        paySignReqHandler.SetParameter("noncestr", nonceStr);
        paySignReqHandler.SetParameter("timestamp", timeStamp);
        paySignReqHandler.SetParameter("package", packageValue);
        //paySignReqHandler.SetParameter("appid", "wxd930ea5d5a258f4f");
        //paySignReqHandler.SetParameter("appkey", "L8LrMqqeGRxST5reouB0K66CaYAWpqhAVsq7ggKkxHCOastWksvuX1uvmvQclxaHoYd3ElNBrNO2DHnnzgfVG9Qs473M3DTOZug5er46FhuGofumV8H2FVR9qkjSlC5K");
        //paySignReqHandler.SetParameter("noncestr", "e7d161ac8d8a76529d39d9f5b4249ccb");
        //paySignReqHandler.SetParameter("timestamp", "1399514976");
        //paySignReqHandler.SetParameter("traceid", "test_1399514976");
        //paySignReqHandler.SetParameter("package", "bank_type=WX&body=%E6%94%AF%E4%BB%98%E6%B5%8B%E8%AF%95&fee_type=1&input_charset=UTF-8&notify_url=http%3A%2F%2Fweixin.qq.com&out_trade_no=7240b65810859cbf2a8d9f76a638c0a3&partner=1900000109&spbill_create_ip=196.168.1.1&total_fee=1&sign=7F77B507B755B3262884291517E380F8");
        paySign = paySignReqHandler.CreateSHA1Sign();



        //获取debug信息,建议把请求和debug信息写入日志，方便定位问题
        //string pakcageDebuginfo = packageReqHandler.getDebugInfo();
        //Response.Write("<br/>pakcageDebuginfo:" + pakcageDebuginfo + "<br/>");
        //string paySignDebuginfo = paySignReqHandler.getDebugInfo();
        //Response.Write("<br/>paySignDebuginfo:" + paySignDebuginfo + "<br/>");


    }
}