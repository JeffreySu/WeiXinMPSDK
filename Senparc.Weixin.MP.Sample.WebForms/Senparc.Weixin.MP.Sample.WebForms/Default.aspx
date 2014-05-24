<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Senparc.Weixin.MP.Sample.WebForms._Default" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>Senparc.Weixin.MP - 微信公众平台SDK</title>
    <link href="http://weixin.senparc.com/Content/site.css" rel="stylesheet" />
    <script src="http://weixin.senparc.com/Scripts/jquery-1.7.1.js"></script>
</head>
<body>
    <header>
        <div class="content-wrapper">
            <div class="float-left">
                <p class="site-title">
                    <a href="/">Senparc.Weixin.MP - 微信公众平台SDK</a>
                </p>
            </div>
            <div class="float-right">
                <nav>
                    <ul id="menu">
                        <li><a href="/">首页</a></li>
                        <li><a href="http://weixin.senparc.com/Menu">自定义菜单设置工具</a></li>
                    </ul>
                </nav>
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
                    Nuget项目：<a href="https://www.nuget.org/packages/Senparc.Weixin.MP" target="_blank">https://www.nuget.org/packages/Senparc.Weixin.MP</a>
                    及 <a href="https://www.nuget.org/packages/Senparc.Weixin.MP.MVC" target="_blank">https://www.nuget.org/packages/Senparc.Weixin.MP.MVC</a><br />
                    系列教程：<a href="http://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html" target="_blank">打开</a><br />
                </p>
                <p>
                    当前Demo运行的Senparc.Weixin.MP.dll版本：<a href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.BuildOutPut" target="_blank">v<%= DllVersion %></a><br />
                </p>
                <p>
                    技术交流QQ群：1群：300313885（已满） 2群：293958349（已满）  3群：342319110（已满） 4群：372212092 <a target="_blank" href="http://shang.qq.com/wpa/qunwpa?idkey=b1732e72573df75bec61b08a2239a33ddc8f4577b541a7ff7bbb4292dcd4aed6">
                        <img border="0" src="http://pub.idqqimg.com/wpa/images/group.png" alt="Senparc.Weixin.MPSDK 4群" title="Senparc.Weixin.MPSDK 4群"></a>
                </p>
                <p>
                    业务联系QQ：498977166
                </p>
                <p>
                    客服热线：400-031-8816
                </p>
                <p>
                    如果这个项目对您有用，我们欢迎各方任何形式的捐助，也包括参与到项目代码更新或意见反馈中来。谢谢！<br />
                    资金捐助（支付宝钱包扫一扫）：
                </p>
                <p>
                    <img src="http://weixin.senparc.com/Images/T1nAXdXb0jXXXXXXXX.png" width="200" />
                </p>

                <p>
                    &nbsp;
                </p>
                辅助工具：<h2><a href="http://weixin.senparc.com/Menu">自定义菜单设置工具</a></h2>
            </div>
        </section>

        <section class="content-wrapper main-content clear-fix">

            <h3>关注官方公众平台微信，进行互动测试：</h3>
            <p>
                <img src="http://weixin.senparc.com/Images/qrcode.jpg" width="259" />
            </p>
            <p>
                目前Senparc.Weixin.MP已支持微信5.0 API中所有接口（包括自定义菜单）。<a href="http://mp.weixin.qq.com/wiki/index.php?title=%E6%B6%88%E6%81%AF%E6%8E%A5%E5%8F%A3%E6%8C%87%E5%8D%97" target="_blank">查看官方API</a>
            </p>
            <ol class="round">
                <li class="one">
                    <h5>文本测试</h5>
                    随意输入文本信息，系统将自动回复一条包含原文的文本信息。<br />
                    如果连续发送多条信息，系统会自动记录通讯上的下文，直到超过规定时间，记录自动清空。
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
                    <h5>视频测试</h5>
                    发送一条视频信息，系统将自动回复一条带有视频ID的信息。
                </li>
                <li class="six">
                    <h5>订阅测试</h5>
                    订阅（关注）账号的第一时间，系统将发送一条欢迎信息（等同于之前的Hello2BizUser）。
                </li>
                <li class="seven">
                    <h5>客户端约束测试</h5>
                    发送文字信息【约束】，进行测试。
                </li>
                <li class="eight">
                    <h5>自定义菜单测试</h5>
                    点击自定菜单进行测试。
                </li>
                <li class="nine">
                    <h5>代理+托管测试</h5>
                    <ol>
                        <li>发送文字信息【代理】或【托管】，或点击菜单【代理消息】&gt;【返回图文】，服务器将从其他微信平台获取“代理”或“托管”文字请求的结果。
                        </li>
                        <li>点击菜单【代理消息】&gt;【会员信息】，查看自己的会员信息（来自另外一台Souidea服务器的微信会员系统）
                        </li>
                    </ol>
                </li>
            </ol>
        </section>

        <section class="content-wrapper clear-fix" id="weiweihi">
            <h3>微信营销生态圈——微微嗨：</h3>
            <p>
                <img src="http://weixin.senparc.com/Images/WeiWeiHiQrCode.jpg" width="259" />
            </p>
            <p><strong><a href="http://www.weiweihi.com">http://www.weiweihi.com</a>，微微嗨是在Senparc.Weixin.MP及Senparc.Weixin.MP P2P核心基础上开发的微信服务平台，联合数万开发者和代理商，打造一流的微信营销生态圈。期待您的加入！</strong></p>
            <p>
                <strong>申请成为微微嗨代理：<a href="http://www.weiweihi.com/AgentApply">http://www.weiweihi.com/AgentApply</a></strong>
            </p>
        </section>

        <!--
<section class="content-wrapper clear-fix" id="souidea">
    <h3>关注Souidea公众平台微信，测试Senparc.Weixin.MP.P2P：</h3>
    <p>
        <img src="http://weixin.senparc.com/Images/qrcode_for_souidea.jpg" width="259" />
    </p>
    <p><strong>现在Souidea平台已经开放：<a href="http://www.souidea.com">http://www.souidea.com</a>，可以自行注册并对接，甚至可以不需要编写一行代码，直接将微信账号对接到这个平台上，轻松整合关键字回复、菜单管理、会员管理，以及众多微信营销工具。</strong></p>

    <p>
        Souidea是用于全面测试<a href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.P2P" target="_blank">Senparc.Weixin.MP和Senparc.Weixin.MP.P2P</a>的第一代机器人，由任务驱动，异步反馈。<br />
        已经对接到微信公众账号。<br />

        主要特点：1、异步任务驱动（异步推送信息） 2、微信公众账号身份信息识别 3、企业级并发任务处理（欢迎疯狂蹂躏）。可以发送IP、文字进行测试。<br />
        <br />
        可以发送如下内容进行测试（由于异步任务驱动，可以不必等待回复，连续发送内容，查看对应任务处理情况）：
    </p>
    <ol class="round">
        <li class="one">
            <h5>IPv4地址</h5>
            对ip进行ping并异步返回结果。
        </li>

        <li class="two">
            <h5>其他文字</h5>
            对文字进行搜索并返回推荐结果。
        </li>
        <li class="three">
            <h5>图片信息</h5>
            搜索相近图片相关信息。
        </li>
        <li class="four">
            <h5>人工客服</h5>
            输入文字【0】进入人工客服呼叫中心。
        </li>
    </ol>
</section>
-->

    </div>

    <footer>
        <p>新浪微博：<a href="http://weibo.com/jeffreysu1984" target="_blank">@苏震巍</a> QQ：498977166</p>
        <p>&copy;2006-2014 Senparc</p>
    </footer>
    <div style="display: none;">
        <script>
            var _hmt = _hmt || [];
            (function () {
                var hm = document.createElement("script");
                hm.src = "//hm.baidu.com/hm.js?a8ad9ff7b9dd4cbb5510f6a929ba085f";
                var s = document.getElementsByTagName("script")[0];
                s.parentNode.insertBefore(hm, s);
            })();
        </script>
    </div>
</body>
</html>
