<%@ Page Language="C#" AutoEventWireup="true" CodeFile="native.aspx.cs" Inherits="native" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <!--https://chart.googleapis.com/chart?cht=qr&chs=200×200&choe=UTF-8&chld=L|4&chl=http://Codeup.org -->

    <div>
    
    <center><a href="<%= parm %>">点击支付(微信浏览器)</a><br>扫描支付</br><img src="<%= parm%>" alt="QR code"/></center>
    </div>
    </form>
</body>
</html>
