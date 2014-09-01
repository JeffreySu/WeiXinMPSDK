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
using Senparc.Weixin.MP.TenPayLib;
//=================================
//原生支付获取订单信息返回的xml
//=================================
public partial class nativecall : System.Web.UI.Page
{
    private static TenPayInfo _tenPayInfo;

    public static TenPayInfo TenPayInfo
    {
        get
        {
            if (_tenPayInfo == null)
            {
                _tenPayInfo =
                    TenPayInfoCollection.Data[System.Configuration.ConfigurationManager.AppSettings["WeixinPay_PartnerId"]];
            }
            return _tenPayInfo;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string sp_billno = Request["order_no"];
        //当前时间 yyyyMMdd
        string date = DateTime.Now.ToString("yyyyMMdd");
        //订单号，此处用时间和随机数生成，商户根据自己调整，保证唯一
        string out_trade_no = "" + DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);

        if (null == sp_billno)
        {
            //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            sp_billno = DateTime.Now.ToString("HHmmss") + TenPayUtil.BuildRandomStr(4);
        }
        else
        {
            sp_billno = Request["order_no"].ToString();
        }

        sp_billno = TenPayInfo.PartnerId + sp_billno;



        //创建RequestHandler实例
        RequestHandler packageReqHandler = new RequestHandler(Context);
        //初始化
        packageReqHandler.Init();
        packageReqHandler.SetKey(TenPayInfo.Key);

        //设置package订单参数
        packageReqHandler.SetParameter("partner", TenPayInfo.PartnerId);		  //商户号
        packageReqHandler.SetParameter("bank_type", "WX");		                      //银行类型
        packageReqHandler.SetParameter("fee_type", "1");                    //币种，1人民币
        packageReqHandler.SetParameter("input_charset", "GBK");
        packageReqHandler.SetParameter("out_trade_no", sp_billno);		//商家订单号
        packageReqHandler.SetParameter("total_fee", "1");			        //商品金额,以分为单位(money * 100).ToString()
        packageReqHandler.SetParameter("notify_url", TenPayInfo.TenPayNotify);		    //接收财付通通知的URL
        packageReqHandler.SetParameter("body", "nativecall");	                    //商品描述
        packageReqHandler.SetParameter("spbill_create_ip", Page.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP

        //获取package包
        string  packageValue = packageReqHandler.GetRequestURL();

        //调起微信支付签名
        string timeStamp = TenPayUtil.GetTimestamp();
        string nonceStr = TenPayUtil.GetNoncestr();

        //设置支付参数
        RequestHandler payHandler = new RequestHandler(Context);
        payHandler.SetParameter("appid", TenPayInfo.AppId);
        payHandler.SetParameter("noncestr", nonceStr);
        payHandler.SetParameter("timestamp", timeStamp);
        payHandler.SetParameter("package", packageValue);
        payHandler.SetParameter("RetCode","0");
        payHandler.SetParameter("RetErrMsg","成功");
        string paySign = payHandler.CreateSHA1Sign();
        payHandler.SetParameter("app_signature", paySign);
        payHandler.SetParameter("sign_method","SHA1");


        Response.ContentType = "text/xml";
        Response.Clear();
        Response.Write(payHandler.ParseXML());

    }
}
