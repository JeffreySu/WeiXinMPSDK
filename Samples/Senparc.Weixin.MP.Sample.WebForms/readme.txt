为了统一MessageHandler方法的调用并得以重用，此WebForms项目引用了 Senparc.Weixin.MP.Sample.CommonService 项目（包含在MVC项目目录中，见 https://github.com/JeffreySu/WeiXinMPSDK/tree/master/Senparc.Weixin.MP.Sample/Senparc.Weixin.MP.Sample.CommonService ），因此打开此项目时请保持上级文件夹目录完整性。

如果部署时出现web.config节点重复的错误，只需要依次删除提示的节点即可（一般是服务器.net版本高于3.5所致）。

如果需要使用通讯日志记录功能，App_Data文件夹也需要一起上传（或新建）。
