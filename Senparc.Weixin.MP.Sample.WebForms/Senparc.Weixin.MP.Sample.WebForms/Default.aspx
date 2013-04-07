<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Senparc.Weixin.MP.Sample.WebForms._Default" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>Senparc.Weixin.MP - 微信公众平台SDK</title>
</head>
<body>
    
<header>
    <div class="content-wrapper">
        <div class="float-left">
            <p class="site-title">
                <a href="/">Senparc.Weixin.MP - 微信公众平台SDK</a>
            </p>
        </div>
    </div>
</header>
<div id="body">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>欢迎使用 微信公众平台SDK!</h1>
                <h2>Senparc.Weixin.MP.dll</h2>
            </hgroup>

            <p>
                使用 Senparc.Weixin.MP.dll 整合网站与微信公众账号的自动交流回复。
            </p>
            <p>
                更多使用说明见：<a href="http://www.cnblogs.com/szw/archive/2013/01/13/senparc-weixin-mp-sdk.html" target="_blank">Senparc.Weixin.MP-微信公众平台SDK（C#）</a><br />
                源代码及示例下载：<a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank">https://github.com/JeffreySu/WeiXinMPSDK</a><br />
            </p>
            <p>
                当前Demo运行的Senparc.Weixin.MP.dll版本：<a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank">v0.3.3.4</a>。
            </p>
            <p>
                技术交流QQ群：300313885
            </p>
        </div>
    </section>
    <section class="content-wrapper main-content clear-fix">
        <h3>关注官方公众平台微信，进行互动测试：</h3>
        <p>
            <img src="http://weixin.senparc.com/Images/qrcode.jpg" />
        </p>
        <p>
            目前Senparc.Weixin.MP已支持微信4.5 API中所有接口。<a href="http://mp.weixin.qq.com/wiki/index.php?title=%E6%B6%88%E6%81%AF%E6%8E%A5%E5%8F%A3%E6%8C%87%E5%8D%97" target="_blank">查看官方API</a>
        </p>
        <ol class="round">
            <li class="one">
                <h5>文本测试</h5>
                随意输入文本信息，系统将自动回复一条包含原文的文本信息。
            </li>

            <li class="two">
                <h5>位置测试</h5>
                发送一条位置信息，系统将自动回复详细的位置信息图片数据及一条图文链接。
            </li>
            <li class="three">
                <h5>图片测试</h5>
                发送一张图片，系统将自动回复一条带链接的图文信息。
            </li>
            <li class="four">
                <h5>语音测试</h5>
                发送一条语音信息，系统将自动回复一条音乐格式信息。
            </li>
            <li class="five">
                <h5>订阅测试</h5>
                订阅（关注）账号的第一时间，系统将发送一条欢迎信息（等同于之前的Hello2BizUser）。
            </li>
        </ol>
    </section>
</div>

    <footer>
        <p>新浪微博：<a href="http://weibo.com/jeffreysu1984" target="_blank">@苏震巍</a> QQ：498977166</p>
        <p>&copy;2013 Senparc</p>
    </footer>
    <script src="/Scripts/jquery-1.7.1.js"></script>

    
</body>
</html>