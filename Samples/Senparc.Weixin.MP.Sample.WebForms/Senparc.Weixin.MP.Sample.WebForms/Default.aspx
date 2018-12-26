<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Senparc.Weixin.MP.Sample.WebForms._Default" %>
<% var domainName = "https://sdk.weixin.senparc.com"; %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>&#x5FAE;&#x4FE1;SDK,&#x5FAE;&#x4FE1;&#x516C;&#x4F17;&#x5E73;&#x53F0;,&#x5C0F;&#x7A0B;&#x5E8F;SDK - Senparc.Weixin SDK - &#x516C;&#x4F17;&#x53F7; &#x4F01;&#x4E1A;&#x53F7; &#x5F00;&#x653E;&#x5E73;&#x53F0; &#x76DB;&#x6D3E;&#x5FAE;&#x4FE1;&#x53F7; - 微信公众平台 小程序 企业号 开放平台 微信支付 JSSDK Senparc.Weixin SDK</title>
    <meta name="keywords" content="微信SDK,微信公众账号,小程序,SDK,微信公众平台,企业号,服务号,订阅号,微信支付,开放平台,Senparc.Weixin,,Senparc.Weixin.MP,JSSDK,开发者,第三方工具,说明,文档,AppStore,微微嗨,盛派网络,Senparc,.net,c#,小程序开发" />
    <meta name="description" content="Senparc.Weixin SDK，是目前使用率超过 90% 的微信 C# SDK，包含完整的公众账号、小程序、企业号、开放平台API，帮助第三方开发者轻松实现微信公众账号和企业号开发。目前包含了Senparc.Weixin.MP（微信公众号SDK）、Senparc.Weixin.Work（微信企业号/企业微信SDK）、Senparc.Weixin.Open（微信开放平台SDK），以及Senparc.Weixin.TenPay（微信支付）、微信JSSDK的完整SDK和案例。" />


    <link href="<%= domainName %>/Content/danktooltip/css/darktooltip.min.css" rel="stylesheet" />

    <link href="<%= domainName %>/Content/reset.css" rel="stylesheet" />
    <link href="<%= domainName %>/Content/style.css" rel="stylesheet" />



    
    
        <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-2.2.0.min.js" crossorigin="anonymous" integrity="sha384-K+ctZQ+LL8q6tP7I94W+qzQsfRV2a+AfHIi9k8z8l9ggpc8X+Ytst4yBo/hH+8Fk">
        </script>
<script>(window.jQuery||document.write("\u003Cscript src=\u0022\/lib\/jquery\/dist\/jquery.min.js\u0022 crossorigin=\u0022anonymous\u0022 integrity=\u0022sha384-K\u002BctZQ\u002BLL8q6tP7I94W\u002BqzQsfRV2a\u002BAfHIi9k8z8l9ggpc8X\u002BYtst4yBo\/hH\u002B8Fk\u0022\u003E\u003C\/script\u003E"));</script>
        <script src="https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/bootstrap.min.js" crossorigin="anonymous" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa">
        </script>
<script>(window.jQuery && window.jQuery.fn && window.jQuery.fn.modal||document.write("\u003Cscript src=\u0022\/lib\/bootstrap\/dist\/js\/bootstrap.min.js\u0022 crossorigin=\u0022anonymous\u0022 integrity=\u0022sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa\u0022\u003E\u003C\/script\u003E"));</script>
        <script src="<%= domainName %>/js/site.min.js?v=47DEQpj8HBSa-_TImW-5JCeuQeRkm5NMpJWZG3hSuFU"></script>
    
    <script src="<%= domainName %>/Content/danktooltip/js/jquery.darktooltip.min.js"></script>
    <script src="<%= domainName %>/Scripts/global.js"></script>
    <script src="<%= domainName %>/Scripts/jquery.form.js"></script>
    
</head>
<body>
    <div class="content">
        <div class="senparc-header">
            <div class="wrapper">
                <div class="logo">
                    <a href="http://www.senparc.com">
                        <img src="<%= domainName %>/images/v2/logo .png" alt="微信SDK" />
                    </a>
                </div>
                <div class="header-title">
                    <a href="<%= domainName %>/" title="微信SDK（公众平台/小程序/企业号/开放平/微信支付/JSSDK/摇一摇周边）">微信SDK（公众平台/小程序/企业号/开放平台/微信支付/JSSDK/摇一摇周边）</a>
                </div>
                <div class="navbar-collapse">
                    <ul class="nav-catalog">
                        <li><a href="<%= domainName %>/">首页</a></li>
                        <li class="btn-top-menu">
                            <a href="javascript:;">工具箱</a>
                            <ul class="nav-sub-catalog">
                                <li><a href="<%= domainName %>/Home/WeChatSampleBuilder">微信 Sample 项目自动生成器 （新）</a></li>
                                <li><a href="<%= domainName %>/Menu">自定义菜单设置</a></li>
                                <li><a href="<%= domainName %>/SimulateTool">消息模拟器</a></li>
                                <li><a href="<%= domainName %>/Cache/Test">缓存测试</a></li>
                                <li><a href="<%= domainName %>/AsyncMethods">异步方法/模板消息测试</a></li>
                                <li><a href="<%= domainName %>/OpenOAuth/JumpToMpOAuth">开放平台授权测试</a></li>
                                <li><a href="<%= domainName %>/Plugins">扩展插件</a></li>
                                <li><a href="https://mp.weixin.qq.com/debug/cgi-bin/sandboxinfo?action=showinfo&t=sandbox/index" target="_blank">测试号入口</a></li>
                            </ul>
                        </li>
                        <li class="btn-top-menu">
                            <a href="https://weixin.senparc.com/QA" target="_blank">问答社区</a>
                            <ul class="nav-sub-catalog">
                                <li><a href="https://weixin.senparc.com/QA?catalog=1" target="_blank">微信开发</a></li>
                                <li><a href="https://weixin.senparc.com/QA?catalog=2" target="_blank">NeuChar 纽插</a></li>
                                <li><a href="https://weixin.senparc.com/" target="_blank">全部</a></li>
                            </ul>
                        </li>

                        <li class="btn-top-menu">
                            <a href="javascript:;">官方教程</a>
                            <ul class="nav-sub-catalog">
                                <li><a href="https://book.weixin.senparc.com/book/videolinknetease?code=sdk-top-menu" target="_blank">官方视频教程</a></li>
                                <li><a href="https://book.weixin.senparc.com/book/link?code=sdk-top-menu" target="_blank">官方正版图书</a></li>
                                <li><a href="https://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html" target="_blank">博客系列教程</a></li>
                            </ul>
                        </li>

                        <li class="btn-top-menu">
                            <a href="https://sdk.weixin.senparc.com/Document">帮助文档</a>
                            <ul class="nav-sub-catalog">
                                <li><a href="https://sdk.weixin.senparc.com/Document#doc-download">离线帮助文档下载</a></li>
                                <li><a href="https://sdk.weixin.senparc.com/Document#doc-online">在线文档</a></li>
                                <li><a href="https://sdk.weixin.senparc.com/Document#doc-online-source-code">查看在线文档源代码</a></li>
                                <li><a href="https://github.com/JeffreySu/WeixinResource" target="_blank">更多开发资源</a></li>
                            </ul>
                        </li>
                        <li><a href="https://www.neuchar.com" target="_blank">NeuChar 纽插</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <!-- 公告 -->
        <div id="noticearea">
            <div class="wrapper">
                <span>
                    Sample 项目自动生成工具测试版已发布。<a href="<%= domainName %>/Home/WeChatSampleBuilder">【点击这里查看】</a>。<br />
                    Senparc 官方微信开发视频教程《微信公众号+小程序快速开发》已经上线，陆续更新中，<a href="https://book.weixin.senparc.com/book/videolinknetease?code=sdk-banner" target="_blank">【点击这开始学习】</a>。<br />
                    Senparc 官方微信开发教程《微信开发深度解析：公众号、小程序高效开发秘籍》已经出版（第3次印刷），<a href="https://book.weixin.senparc.com/book/link?code=sdk-banner" target="_blank">【点击这里购买正版】</a>。<br />
                </span>
            </div>
        </div>
            <div class="senparc-jumbotron">
                <div class="wrapper">
                    <div class="container-left">
                        <ul>
                            <li><a href="https://weixin.senparc.com">Senparc.Weixin SDK 官网</a></li>
                            <li><a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank">源代码及示例下载</a></li>
                            <li><a href="https://weixin.senparc.com/QA" target="_blank">微信技术交流社区</a></li>
                            <li><a href="https://book.weixin.senparc.com/book/link?code=sdk-common-banner" target="_blank">购买官方教程图书</a></li>
                            <li><a href="http://study.163.com/course/introduction/1004873017.htm" target="_blank">观看官方视频教程</a></li>
                            <li><a href="https://book.weixin.senparc.com/book/videolinknetease?code=sdk-top-side" target="_blank">购买官方视频教程</a></li>
                            <li><a href="https://sdk.weixin.senparc.com/Document">下载帮助文档</a></li>
                            <li><a href="http://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html" target="_blank">简易入门教程</a></li>
                            <li><a href="https://github.com/JeffreySu/WeixinResource" target="_blank">微信开发资源汇总</a></li>
                        </ul>
                    </div>
                    <div class="container-right">
                        <span class="container-span container-span-title">微信公众平台SDK</span>
                        <span class="container-span">
                            <span>全面支持：微信公众号、小程序、微信支付、</span>
                            <span>JS-SDK、开放平台、企业号、企业微信……</span>
                        </span>
                        <span class="container-p">Senparc.Weixin SDK 是目前使用率最高的微信 .NET SDK，</span>
                        <span class="container-p">也是最受欢迎的 .NET 开源项目之一。</span>
                        <span class="container-p">快来使用 Senparc.Weixin SDK 轻松打造微信各平台的扩展应用吧！</span>
                        <span class="container-p"> &nbsp; </span>
                        <span class="container-p">现在起您还可以使用 NeuChar 帮您轻松跨平台开发和配置！ <a href="https://neuchar.com" target="_blank" class="application">立即开始</a></span>
                        <div class="contact">
                            <div class="clear"></div>
                            <div id="qqGroups">QQ群载入中……</div>
                            <div class="contatc-name">400客服热线：400-031-8816</div>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        <div class="wrapper">
    <div class="weixin-bottom">
        <p class="weixin-item-title">
            目前 Senparc.Weixin SDK 已支持微信 6.x API 中所有接口以及<strong>小程序</strong>
        </p>

        <div class="weixin-bottom-buttons">
            <p class="weixin-item-button">
                <a href="https://book.weixin.senparc.com/book/link?code=sdk-homepage" class="application" target="_blank">购买正版 Senparc 官方教程</a>
                &nbsp; &nbsp;
                <a href="https://book.weixin.senparc.com/book/videolinknetease?code=sdk-home-top-button" class="application" target="_blank">观看官方视频教程</a>
                &nbsp; &nbsp; &nbsp;

                <a href="<%= domainName %>/Home/Book" class="application" target="_blank">官方教程配套阅读系统</a>
                &nbsp; &nbsp; &nbsp;

            </p>
            <p class="weixin-item-button" style="padding-top:10px;">

                <a href="https://sdk.weixin.senparc.com/Document" class="application" target="_blank">下载 SDK 帮助文档</a>
                &nbsp; &nbsp; &nbsp;

                <a href="https://github.com/JeffreySu/WeiXinMPSDK" class="application" target="_blank">GitHub 源码</a>
                &nbsp; &nbsp; &nbsp;

                <a href="http://mp.weixin.qq.com/wiki/index.php?title=%E6%B6%88%E6%81%AF%E6%8E%A5%E5%8F%A3%E6%8C%87%E5%8D%97" class="application" target="_blank">微信官方API</a>
            </p>
        </div>
        <div class="clear"></div>
        <p class="weixin-item-title">

            <a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank"><img src="https://img.shields.io/github/watchers/JeffreySu/WeixinMPSDK.svg?style=social&label=Watch" /></a>


            <a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank"><img src="https://img.shields.io/github/stars/JeffreySu/WeixinMPSDK.svg?style=social&label=Star" /></a>


            <a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank"><img src="https://img.shields.io/github/forks/JeffreySu/WeixinMPSDK.svg?style=social&label=Fork" /></a>


            <a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank"><img src="https://img.shields.io/github/followers/JeffreySu.svg?style=social&logo=github&label=Follow" /></a>
        </p>

        <div class="weixin-bottom-table" id="nuget-versions">
            <table class="tbVersion">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>功能模块</th>
                        <th>Nuget 文件名</th>
                        <th>Nuget 版本</th>
                        <th>当前站点运行版本</th>
                        <th>.NET 3.5</th>
                        <th>.NET 4.0</th>
                        <th>.NET 4.5</th>
                        <th>.NET Core 2.0</th>
                        <th>.NET Core 2.1</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>CO2NET<br />公共基础库</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.CO2NET" target="_blank">
                                Senparc.CO2NET.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.CO2NET" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.CO2NET.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v0.4.1</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>SDK 公共基础库</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin" target="_blank">
                                Senparc.Weixin.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v6.3.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>微信支付  <span style="color:red">new</span></td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.TenPay" target="_blank">
                                Senparc.Weixin.TenPay.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.TenPay" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.TenPay.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v1.1.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>4</td>
                        <td>公众号<br />JSSDK<br />摇一摇周边</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.MP" target="_blank">
                                Senparc.Weixin.MP.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.MP" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.MP.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v16.6.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td>小程序 <span style="color:red">HOT</span></td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.WxOpen" target="_blank">
                                Senparc.Weixin.WxOpen.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.WxOpen" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.WxOpen.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WxOpen/" target="_blank">v3.3.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>6</td>
                        <td>开放平台 SDK</td>
                        <td><a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Open" target="_blank">Senparc.Weixin.Open.dll</a></td>
                        <td><a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Open" target="_blank"><img src="https://img.shields.io/nuget/v/Senparc.Weixin.Open.svg?style=flat" /></a></td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v4.3.1</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>7</td>
                        <td>企业微信 SDK</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Work" target="_blank">
                                Senparc.Weixin.Work.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Work" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.Work.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v3.3.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>8</td>
                        <td>MVC 扩展插件</td>
                        <td><a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.MP.MVC" target="_blank">Senparc.Weixin.MP.MvcExtentsion.dll</a></td>
                        <td><a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.MP.MVC" target="_blank"><img src="https://img.shields.io/nuget/v/Senparc.Weixin.MP.Mvc.svg?style=flat" /></a></td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v7.2.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-Y-brightgreen.svg" /></td>
                        <td>
                            <img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" />
                        </td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>9</td>
                        <td>Redis 缓存</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Cache.Redis" target="_blank">
                                Senparc.Weixin.Cache.Redis.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Cache.Redis" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.Cache.Redis.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v2.4.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>10</td>
                        <td>Memcached 缓存</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Cache.Memcached" target="_blank">
                                Senparc.Weixin.Cache.Memcached.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.Weixin.Cache.Memcached" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.Weixin.Cache.Memcached.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/src/Senparc.Weixin.MP.BuildOutPut" target="_blank">v2.3.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
                    <tr>
                        <td>11</td>
                        <td>WebSocket 模块</td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.WebSocket" target="_blank">
                                Senparc.WebSocket.dll
                            </a>
                        </td>
                        <td>
                            <a class="sdk-version" href="https://www.nuget.org/packages/Senparc.WebSocket" target="_blank">
                                <img src="https://img.shields.io/nuget/v/Senparc.WebSocket.svg?style=flat" />
                            </a>
                        </td>
                        <td><a class="sdk-version" href="https://github.com/JeffreySu/Senparc.WebSocket/" target="_blank">v0.6.0</a></td>
                        <td><img title=".NET 3.5" alt=".NET 3.5" src="https://img.shields.io/badge/3.5-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.0" alt=".NET 4.0" src="https://img.shields.io/badge/4.0-N-lightgrey.svg" /></td>
                        <td><img title=".NET 4.5" alt=".NET 4.5" src="https://img.shields.io/badge/4.5-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.0" alt=".NET Core" src="https://img.shields.io/badge/core2.0-Y-brightgreen.svg" /></td>
                        <td><img title=".NET Core 2.1" alt=".NET Core 2.0" src="https://img.shields.io/badge/core2.1-Y-brightgreen.svg" /></td>
                    </tr>
            </table>
        </div>
        <div class="clear"></div>
    </div>
    <div class="content-weixin">
        <p class="weixin-item-title">关注官方微信 进行互动测试</p>

        <div class="weixin-item weixin-item-bs weixin-item-left">
            <img class="official-img" src="<%= domainName %>/images/v2/ewm_01.png" alt="微信SDK公众号" />
        </div>

        <div class="weixin-item weixin-item-bs weixin-item-left">
            <img class="official-img" src="<%= domainName %>/images/SenparcRobot_MiniProgram.jpg" alt="微信SDK小程序" />
        </div>

        <div class="weixin-item  readmore">
            <table class="tbVersion tbVersion_bootstrap">
                <tr>
                    <td>运行中的缓存框架：Local</td>
                    <td><a class="sdk-version" href="<%= domainName %>/Cache/Test">&#x6D4B;&#x8BD5;</a></td>
                </tr>
                <tr>
                    <td>最新文档版本（MP）：v16.5.0</td>
                    <td><a class="sdk-version" href="<%= domainName %>/Document">&#x4E0B;&#x8F7D;</a></td>
                </tr>
                <tr>
                    <td>GitHub 源码：<img src="https://travis-ci.org/JeffreySu/WeiXinMPSDK.svg?branch=master"></td>
                    <td>
                        <a href="https://github.com/JeffreySu/WeiXinMPSDK" class="sdk-version" target="_blank">
                            查看
                        </a>
                    </td>
                </tr>
                <tr>
                    <td>小程序：<img src="https://travis-ci.org/JeffreySu/WxOpen.svg?branch=master"></td>
                    <td>
                        <a href="https://github.com/JeffreySu/WxOpen" class="sdk-version" target="_blank">
                            查看
                        </a>
                    </td>
                </tr>
                <tr>
                    <td>WebSocket：<img src="https://travis-ci.org/JeffreySu/WebSocket.svg?branch=master"></td>
                    <td>
                        <a href="https://github.com/JeffreySu/Senparc.WebSocket" class="sdk-version" target="_blank">
                            查看
                        </a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear"></div>
    </div>
    <div class="clear"></div>
    <div class="line"></div>

    <div id="showTest">
        ⇩ 展开公众号测试说明 ⇩
    </div>

    <div class="test test_bs" id="testTip">
        <table>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_01.png" />
                        <p class="item-title">文本测试</p>
                        <p class="item-content">随意输入文本信息，系统将自动回复一条包含原文的文本信息。如果连续发送多条信息，系统会自动记录通讯的下文，直到超过规定时间记录自动清空。</p>
                    </div>
                </td>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_02.png" />
                        <p class="item-title">位置测试</p>
                        <p class="item-content">发送一条位置信息，系统将自动回复详细的位置信息图片数据及一条图文链接。</p>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_03.png" />
                        <p class="item-title">图片测试</p>
                        <p class="item-content">发送一张图片，系统将自动回复一条带链接的图文信息。</p>
                    </div>
                </td>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_04.png" />
                        <p class="item-title">语音测试</p>
                        <p class="item-content">发送一条语音信息，系统将自动回复一条音乐格式信息。</p>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_05.png" />
                        <p class="item-title">视频测试</p>
                        <p class="item-content">发送一条视频信息，系统将自动回复一条带有视频ID的信息。</p>
                    </div>
                </td>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_06.png" />
                        <p class="item-title">订阅测试</p>
                        <p class="item-content">订阅（关注）账号的第一时间，系统将发送一条欢迎信息（等同于之前的Hello2BizUser）</p>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_07.png" />
                        <p class="item-title">客服端约束测试</p>
                        <p class="item-content">发送文字信息【约束】，进行测试。</p>
                    </div>
                </td>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_08.png" />
                        <p class="item-title">自定义菜单测试</p>
                        <p class="item-content">点击自定菜单进行测试</p>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_09.png" />
                        <p class="item-title">代理+托管测试</p>
                        <p class="item-content">1.发送文字信息【代理】或【托管】，或点击菜单【功能体验】【托管】，服务器将从其他微信平台获取“代理”或“托管”文字请求的结果。2.点击菜单【功能体验】>【会员消息】，查看自己的会员信息（来自另外一台 WeiWeiHi 服务器的微信会员系统）</p>
                    </div>
                </td>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_12.png" />
                        <p class="item-title">微信支付测试</p>
                        <p class="item-content">点击菜单【功能体验】 【微信支付】，体验微信支付整个过程。</p>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_13.png" />
                        <p class="item-title">微信弹出拍照或相册测试</p>
                        <p class="item-content">点击菜单【二级菜单】 【拍照或相册】，弹出拍照或从相框选择对话框，发送图片。</p>
                    </div>
                </td>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_14.png" />
                        <p class="item-title">微信扫码测试</p>
                        <p class="item-content">点击菜单【二级菜单】 【微信扫码】，进入微信扫码界面。</p>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <div class="line"></div>
    <div class="weixin-weiweihi">
        <div class="weixin-item">
            <div class="weixin-item-content">
                <p class="weixin-item-title">基于 Senparc SDK 开发的“微微嗨会议智能助手”</p>
                <p class="weixin-item-font">“微微嗨会议智能助手”是在 Senparc.Weixin.MP + Open + Redis 等模块的基础上研发的实时场景互动云平台，为各类会议、活动、婚庆、教育等不同场景提供组织管理及多屏实时互动服务。欢迎体验！</p>
            </div>
            <div class="weixin-item-img">
                <img src="<%= domainName %>/images/v2/ewm_02.png" width="250" />
            </div>
            <a href="javascript:;"></a>
        </div>
        <div class="clear"></div>
    </div>
</div>


        <div class="feedback">
            <div class="wrapper">
                <div id="book-title">Senparc 官方微信开发教程：《微信开发深度解析：公众号、小程序高效开发秘籍》</div>
                <div class="feedback-info">
                    <div class="crowdfunding-note center">
                        <img src="<%= domainName %>/images/book-cover-front-small-3d-transparent.png" />


                    </div>
                </div>
                <div class="feedback-action">
                    <p class="crowdfunding-note">
                        &nbsp; &nbsp; &nbsp; 为了将我们积累的经验更多、更系统地与开发者分享，由 Senparc.Weixin SDK 作者耗时 2 年，亲自整理编写的《微信开发深度解析：公众号、小程序高效开发秘籍》已经出版。<br />
                        &nbsp; &nbsp; &nbsp;全书涵盖从微信的基础接口介绍、Senparc.Weixin SDK 的深入剖析，以及我们在研究过程中发现的许多微信开发“坑”，都一一向读者介绍，旨在将更多关于微信开发的精华以及架构思想分享给开发者。<br />
                        &nbsp; &nbsp; &nbsp; 感谢您对 Senparc 的支持！<br /><br />

                    </p>
                    <p class="center">
                        <a href="https://book.weixin.senparc.com/book/link?code=sdk-footer" class="application" target="_blank">购买正版 Senparc 官方教程</a><br /><br />
                    </p>


                </div>
            </div>
            <div class="segregate"></div>
            <div class="wrapper">
                <div id="book-title">Senparc官方微信开发视频教程：《微信公众号+小程序快速开发》</div>
                <div class="feedback-info">
                    <div class="crowdfunding-note center">
                        <img src="<%= domainName %>/Images/sdkCourse.jpg" />
                    </div>
                </div>
                <div class="feedback-action">
                    <p class=" top">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;为了帮助大家更快速的掌握微信及更多开发技能，盛派网络成立了“盛派课堂”团队，制作首个线上视频课程《微信公众号+小程序快速开发》，由《微信开发深度解析》图书的作者苏震巍主讲。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;课程采用“理论+实战”的教学形式，结合部分《微信开发深度解析》内容，独立于书本，包含了更多的操作演示和案例展示，帮助大家从多个维度学习微信开发以及.NET开发过程中的诸多技巧。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;课程包含两大部分：<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1. 微信开发基础技能<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. 公众号及小程序案例实战<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;共计60课时，配有番外篇，目前视频已在网易云课堂上线。<br />
                    </p>
                    <p class="center textTop">
                        <a href="https://book.weixin.senparc.com/book/videolinknetease?code=sdk-footer" class="application" target="_blank">点击观看</a><br /><br />
                    </p>


                </div>
            </div>
            <div class="segregate"></div>
            <div class="wrapper" id="donate">
                <div class="feedback-info feedback-info-bs">
                    <p>如果这个项目对你有用，</p>
                    <p>我们欢迎各方任何形式的捐助，</p>
                    <p>也包括参与到项目代码更新或意见反馈中来。</p>
                    <p>谢谢！</p>
                </div>
                <div class="feedback-action contentTop">
                    <img src="<%= domainName %>/images/v2/ewm_03.png" />
                    <p>资金捐助（支付宝钱包扫一扫）</p>
                </div>
            </div>

        </div>
        <div class="footer">
            <p class="footer-contact">
                &nbsp;

                当前框架：.NET Core 2.1
            </p>
            <p class="footer-icon">&copy; 2006-2018 Senparc，苏州盛派网络科技有限公司，版权所有，保留所有权利。</p>
        </div>
        
    <script>
        $(function () {
            $(".test table td:not(:empty)").hover(function () {
                $(this).addClass('currentTestItem');
            },
                function () {
                    $(this).removeClass('currentTestItem');
                });

            $('#showTest').click(function () {
                $(this).fadeOut(function () { $('#testTip').slideDown(); });
            });
        });
    </script>

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
    </div>
</body>
</html>