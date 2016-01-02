<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Senparc.Weixin.MP.Sample.WebForms._Default" %>
<% var domainName = "http://weixin.senparc.com"; %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>Senparc.Weixin.MP - 微信公众平台SDK</title>
    <meta name="keywords" content="微信SDK,微信公众账号,SDK,微信公众平台,Senparc.Weixin,开发者,第三方工具,AppStore,微微嗨,盛派网络,Senparc" />
    <meta name="description" content="Senparc.Weixin，使用率最高的微信公众账号/企业号 C# SDK，帮助第三方开发者轻松实现微信公众账号和企业号开发。" />
    <link href="<%= domainName %>/Content/css?v=vtgTjlCxGfgXkawcGk68h72BbTEmgKiuVRyGhvjniqo1" rel="stylesheet"/>

    <link href="<%= domainName %>/Content/darktooltip.min.css" rel="stylesheet" />
    <script src="<%= domainName %>/bundles/modernizr?v=jmdBhqkI3eMaPZJduAyIYBj7MpXrGd2ZqmHAOSNeYcg1"></script>

    <script src="<%= domainName %>/bundles/jquery?v=VW9pyEu5wNXvHqV5Z1MO5o_3VH7F3gpXdoWotCkzj9k1"></script>

    <script src="<%= domainName %>/Content/danktooltip/js/jquery.darktooltip.min.js"></script>
    
</head>
<body>
    <div class="content">
        <div class="senparc-header">
            <div class="wrapper">
                <div class="logo">
                    <a href="http://www.senparc.com">
                        <img src="<%= domainName %>/images/v2/logo .png" />
                    </a>
                </div>
                <div class="header-title"><a href="/">微信公众平台SDK</a></div>
                <div class="navbar-collapse">
                    <ul class="nav-catalog">
                        <li><a href="/">首页</a></li>
                        <li><a href="<%= domainName %>/Menu">自定义菜单设置工具</a></li>
                        <li><a href="<%= domainName %>/SimulateTool">消息模拟工具</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        <div class="senparc-jumbotron">
            <div class="wrapper">
                <div class="container-left">
                    <ul>
                        <li><a href="https://github.com/JeffreySu/WeiXinMPSDK" target="_blank">源代码及示例下载</a></li>
                        <li><a href="http://www.weiweihi.com/QA" target="_blank">微信技术交流社区</a></li>
                        <li><a href="http://www.cnblogs.com/szw/archive/2013/01/13/senparc-weixin-mp-sdk.html" target="_blank">更多使用说明</a></li>
                        <li><a href="https://www.nuget.org/packages/Senparc.Weixin.MP" target="_blank">Nuget项目</a></li>
                        <li><a href="http://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html" target="_blank">系列教程</a></li>
                    </ul>
                </div>
                <div class="container-right">
                    <span class="container-span">微信公众平台SDK </span>
                    <span class="container-span">Senparc.Weixin.MP.dll &amp; Senparc.Weixin.QY.dll</span>
                    <span class="container-p">使用Senparc.Weixin整合网站与微信公众账号及企业号的自动交流回复。</span>
                    <span class="container-p version">
                        当前版本：<br />
                        Senparc.Weixin.dll <a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.BuildOutPut" target="_blank">v4.2</a><br />
                        Senparc.Weixin.MP.dll <a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.BuildOutPut" target="_blank">v13.2</a><br />
                        MvcExtentsion.dll <a class="sdk-version" href="https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.BuildOutPut" target="_blank">v4.0</a>
                    </span>
                    <div class="contact">
                        <div class="contatc-name">400客服热线：4008-661-666</div>
                        <div class="clear"></div>

                        <div id="qqGroups">QQ群载入中……</div>

                        <div class="clear"></div>

                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
        


<div class="catalog">
    <a href="<%= domainName %>/Menu">自定义菜单设置工具</a>
    <a href="<%= domainName %>/SimulateTool">消息模拟工具</a>
</div>
<div class="wrapper">

    <div class="weixin-bottom">
        <p class="weixin-item-title">目前Senparc.Weixin.MP已支持微信6.x API中所有接口</p>
        <a href="http://mp.weixin.qq.com/wiki/index.php?title=%E6%B6%88%E6%81%AF%E6%8E%A5%E5%8F%A3%E6%8C%87%E5%8D%97" class="application" target="_blank">查看官方API</a>
    </div>

    <div class="content-weixin">
        <div class="weixin-item">
            <p class="weixin-item-title">关注官方微信 进行互动测试</p>
            <img src="<%= domainName %>/images/v2/ewm_01.png" />
        </div>
        <div class="clear"></div>
    </div>

    <div class="test">
        <table>
            <tr>
                <td>
                    <div class="test-item">
                        <img src="<%= domainName %>/images/v2/icon_01.png" />
                        <p class="item-title">文本测试</p>
                        <p class="item-content">随意输入文本信息，系统将自动回复一条包含原文的文本信息。如果连续发送多条信息，系统会自动记录通讯上的下文，知道超过规定时间记录自动清空。</p>
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
                        <p class="item-content">发送一张图片，系统将自动回复一天带链接的图文信息。</p>
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
                        <p class="item-content">1.发送文字信息【代理】或【托管】，或点击菜单【功能体验】【托管】，服务器将从其他微信平台获取“代理”或“托管”文字请求的结果。2.点击菜单【功能体验】>【会员消息】，查看自己的会员信息（来自另外一台Souidea服务器的微信会员系统）</p>
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
            <p class="weixin-item-title">微信营销生态圈—— ——微微嗨</p>
            <div class="weixin-item-content">
                <p class="weixin-item-font">微微嗨是在Senparc.Weixin.MP及Senparc.Weixin.MP P2P核心基础上开发的微信服务平台，联合数万开发者和代理商，打造一流的微信营销生态圈。期待您的加入</p>
                <a href="http://www.weiweihi.com/AgentApply" class="application">申请成为微微嗨代理</a>
            </div>
            <div class="weixin-item-img">
                <img src="<%= domainName %>/images/v2/ewm_02.png" />
            </div>
            <a href="javascript:;"></a>
        </div>
        <div class="clear"></div>
    </div>
</div>

        <div class="feedback">
            <div class="wrapper">
                <div class="feedback-info">
                    <p>如果这个项目对你有用，</p>
                    <p>我们欢迎各方任何形式的捐助，</p>
                    <p>也包括参与到项目代码更新或意见反馈中来。</p>
                    <p>谢谢！</p>
                </div>
                <div class="feedback-action">
                    <img src="<%= domainName %>/images/v2/ewm_03.png" />
                    <p>资金捐助（支付宝钱包扫一扫）</p>
                </div>
            </div>
            <div class="segregate"></div>
            <div class="wrapper">
                <div class="feedback-info">
                    <p>《微信公众平台快速开发》图书众筹</p>
                    <div class="crowdfunding-note">
                        <p class="crowdfunding-note">
                            &nbsp; &nbsp;  &nbsp; 为了将我们积累的经验更多、更系统地与开发者分享，我们已和电子工业出版社签订了约稿协议，书名为《微信公众平台快速开发》（暂定）。全书内容从案例出发，涵盖从微信的基础接口介绍、Senparc.Weixin.MP（微信公众平台SDK）的深入剖析，以及我们在研究过程中发现的许多微信开发“坑”，都一一向读者介绍。<br />
                            &nbsp; &nbsp;  &nbsp; 这是一本饱含诚意的书，由Senparc.Weixin作者苏震巍及核心团队倾情编写，为的是向开发者们分享最原汁原味的Senparc.Weixin架构及微信开发的精髓。<br />
                            &nbsp; &nbsp;  &nbsp; 感谢您对Senparc的支持！<br />
                        </p>
                    </div>
                </div>
                <div class="feedback-action">
                    <img src="<%= domainName %>/images/crowdfunding-qrcode.png" />
                    <p>参与众筹（微信扫一扫）</p>
                </div>
            </div>
        </div>
        <div class="footer">
            <p class="footer-contact">
                新浪微博：
                <a href="http://weibo.com/jeffreysu1984" target="_blank">@苏震巍</a>
                
            </p>
            <p class="footer-icon">copyright &copy; 2006-2015 Senparc，苏州盛派网络科技有限公司</p>
        </div>
        
    <script>
        $(function () {
            $(".test table td:not(:empty)").hover(function () {
                $(this).addClass('currentTestItem');
            }, function () {
                $(this).removeClass('currentTestItem');
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

                $(function () {
                    loadQQGroups();
                });

                function loadQQGroups() {
                    $.ajax({
                        type: "get",
                        async: false,
                        url: "http://www.weiweihi.com/WeixinSdk/GetSdkQqGroupListJson",
                        dataType: "jsonp",
                        jsonp: "callbackparam", //服务端用于接收callback调用的function名的参数
                        jsonpCallback: "success_jsonpCallback", //callback的function名称
                        success: function (json) {
                            $('#qqGroups').html(json[0].html);
                            $('#contact-content li.contact-qq').darkTooltip({
                                theme: 'light'
                            });
                        },
                        error: function () {
                            //alert('fail');
                        }
                    });
                }
            </script>
        </div>
    </div>
</body>
</html>