# WebForms Sample 说明

为了统一 MessageHandler 方法的调用并得以重用，此 WebForms 项目引用了 Senparc.Weixin.MP.Sample.CommonService 项目（包含在 MVC 项目目录中，见 [Senparc.Weixin.MP.Sample.CommonService](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample.CommonService)），因此打开此项目时请保持上级文件夹目录完整性。

如果部署时出现 web.config 节点重复的错误，只需要依次删除提示的节点即可（一般是服务器.net版本高于3.5所致）。

如果需要使用通讯日志记录功能，App_Data 文件夹也需要一起上传（或新建）。

Senparc 推荐您使用 [ASP.NET Core MVC Sample](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Samples/Senparc.Weixin.MP.Sample.vs2017/Senparc.Weixin.MP.CoreSample)，此项目中包含了最全的功能演示，本项目中重点演示了 WebForms 有差异化的部分，无差异部分可以参考 [ASP.NET Core MVC Sample](https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Samples/Senparc.Weixin.MP.Sample.vs2017/Senparc.Weixin.MP.CoreSample)。
