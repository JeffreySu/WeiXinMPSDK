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
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
//=================================
//原生支付
//=================================
public partial class native : System.Web.UI.Page
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

    public String parm;
    //url编码，添加空格转成%20
    public string UrlEncode1(string con)
    {
        string UrlEncode="";
        UrlEncode = HttpUtility.UrlEncode(con, Encoding.UTF8);
        UrlEncode = UrlEncode.Replace("+", "%20");
        return UrlEncode;
    }
//' * google api 二维码生成【QRcode可以存储最多4296个字母数字类型的任意文本，具体可以查看二维码数据格式】
//' * @param string $chl 二维码包含的信息，可以是数字、字符、二进制信息、汉字。不能混合数据类型，数据必须经过UTF-8 URL-encoded.如果需要传递的信息超过2K个字节请使用POST方式
//' * @param int $widhtHeight 生成二维码的尺寸设置
//' * @param string $EC_level 可选纠错级别，QR码支持四个等级纠错，用来恢复丢失的、读错的、模糊的、数据。
//' *                         L-默认：可以识别已损失的7%的数据
//' *                         M-可以识别已损失15%的数据
//' *                         Q-可以识别已损失25%的数据
//' *                         H-可以识别已损失30%的数据
//' * @param int $margin 生成的二维码离图片边框的距离
    public string QRfromGoogle(string chl)
    {
        int widhtHeight = 300;
        string EC_level = "L";
        int margin = 0;
        string QRfromGoogle;
        chl = UrlEncode1(chl);
        QRfromGoogle = "http://chart.apis.google.com/chart?chs=" + widhtHeight + "x" + widhtHeight + "&cht=qr&chld=" + EC_level + "|" + margin + "&chl=" + chl;
        return QRfromGoogle;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string sp_billno = Request["order_no"];
        //当前时间 yyyyMMdd
        string date = DateTime.Now.ToString("yyyyMMdd");

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

        
        RequestHandler outParams = new RequestHandler(Context);
        
        outParams.Init();
        string productid = sp_billno;
        string timeStamp = TenPayUtil.GetTimestamp();
        string nonceStr = TenPayUtil.GetNoncestr();

        RequestHandler Params = new RequestHandler(Context);
        Params.SetParameter("appid", TenPayInfo.AppId);
        Params.SetParameter("appkey", TenPayInfo.AppKey);
        Params.SetParameter("noncestr", nonceStr);
        Params.SetParameter("timestamp", timeStamp);
        Params.SetParameter("productid", productid);
        string sign = Params.CreateSHA1Sign();
        Params.SetParameter("sign",sign);


        parm = "weixin://wxpay/bizpayurl?sign=" + sign + "&appid=" + TenPayInfo.AppId + "&productid=" + productid + "&timeStamp=" + timeStamp + "&nonceStr=" + nonceStr;
        parm = QRfromGoogle(parm);
      
    }
}
