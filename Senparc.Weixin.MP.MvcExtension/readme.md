 Senparc.Weixin.MP.MvcExtension 是针对MVC（4.0）项目的扩展包，可以简化部分MVC中的操作。
 
 说明见：[Senparc.Weixin.MP.MvcExtension.dll使用说明](https://github.com/JeffreySu/WeiXinMPSDK/wiki/Senparc.Weixin.MP.MvcExtension.dll%E4%BD%BF%E7%94%A8%E8%AF%B4%E6%98%8E)
 

 此项基于Senparc.Weixin.MP.dll的支持。


更新日志：

v1.4.16 /2013-12-10

为了处理IOS微信软件下的换行bug，暂时添加了一个FixWeixinBugWeixinResult的返回类型暂时替代原来的WeixinResult。微信官方修复bug后此方法将删除。

v1.4 /2013-11-18

微信内置浏览器判断（WeixinInternalRequestAttribute），添加了一个ignoreParameter参数，用于测试的时候忽略判断。

v1.0 /2013-5-21

完成WeixinInternalRequest及WeixinResult。
