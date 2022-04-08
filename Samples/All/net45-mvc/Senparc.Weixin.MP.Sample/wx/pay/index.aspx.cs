using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("页面跳转调用:</br>");
        Response.Write("<a target=\"_blank\" href=\"" + "jsapi.aspx" + "\">" + "jsapi支付" + "</a></br>");
        Response.Write("<a target=\"_blank\" href=\"" + "native.aspx" + "\">" + "native支付" + "</a></br>");
        Response.Write("<a target=\"_blank\" href=\"" + "nativecall.aspx" + "\">" + "nativecall支付" + "</a></br>");

    }
}