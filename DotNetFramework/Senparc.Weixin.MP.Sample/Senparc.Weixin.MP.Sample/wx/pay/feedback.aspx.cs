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
//维权接口
//=================================
public partial class feedback : System.Web.UI.Page
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
        //创建支付应答对象
        ResponseHandler resHandler = new ResponseHandler(Context);
        resHandler.Init();
        resHandler.SetKey(TenPayInfo.Key, TenPayInfo.AppKey);

        //判断签名
        if (resHandler.IsWXsignfeedback())
        {
            //回复服务器处理成功
            Response.Write("OK");
            Response.Write("OK:" + resHandler.GetDebugInfo());
        }
        else {
            //sha1签名失败
            Response.Write("fail");
            Response.Write("fail:" + resHandler.GetDebugInfo());
        }
        Response.End();
    }
}
